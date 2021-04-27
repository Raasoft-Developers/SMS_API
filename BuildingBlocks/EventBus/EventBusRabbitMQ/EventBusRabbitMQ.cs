using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using EventBus.Subscription;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using EventBus.Extensions;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        string RABITMQ_EXCHANGE = "tv2_event_bus";
        private readonly string _xchangeType;
        private readonly string AUTOFAC_SCOPE_NAME = "tv2_event_bus_scope";
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ISubscriptionManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly int _retryCount;
        private IModel _consumerChannel;
        private string _queueName;
        private readonly BlockingCollection<Dictionary<string, string>> _responseMessage = new BlockingCollection<Dictionary<string, string>>();

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
            ILifetimeScope autofac, ISubscriptionManager subsManager, string queueName = null, int retryCount = 5, 
            string xchangeSuffix = "", string xchangeType = "direct")
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _xchangeType = xchangeType;
            RABITMQ_EXCHANGE += xchangeSuffix;
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _subsManager = subsManager ?? new SubscriptionManager();
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _autofac = autofac;
            _retryCount = retryCount;            
        }

        #region Public method
        public void Publish(IntegrationEvent @event)
        {
            PublishEvent(@event);
        }

        public T Publish<T>(IntegrationEvent @event)
        {
            _logger.LogDebug($"Publish<T> -> EVENTID - {@event.Id}");
            string guidId = Guid.NewGuid().ToString();
            
            PublishEvent(@event, true, guidId);

            string responseMsg = string.Empty;
            Dictionary<string, string> resData;

            while (_responseMessage.TryTake(out resData, -1))
            {
                if (resData.ContainsKey(guidId))
                {
                    responseMsg = resData[guidId];
                    break;
                }
            }
            _logger.LogDebug($"RESPONSE MSG - {responseMsg}");

            var responseData = JsonConvert.DeserializeObject<T>(responseMsg);
            return responseData;
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            DoInternalSubscription(eventName);

            _logger.LogDebug("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            _subsManager.AddSubscription<T, TH>();

            StartBasicConsume();
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }
            _subsManager.Clear();
        }

        #endregion

        #region private methods
        private void PublishEvent(IntegrationEvent @event, bool isResponse = false,string guidId = "")
        {
            _logger.LogDebug("PublishEvent");
            _logger.LogDebug($"EVENTID - {@event.Id}, ISRESPONSE ? {isResponse}, GUIDID {guidId}");

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = string.IsNullOrEmpty(@event.ReplayRoutingId) || isResponse ? @event.GetType().Name : @event.ReplayRoutingId;

             _logger.LogDebug("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);
            
            using (var channel = _persistentConnection.CreateModel())
            {
                 _logger.LogDebug("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                channel.ExchangeDeclare(exchange: RABITMQ_EXCHANGE, type: _xchangeType);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                string replyQueueName = "";

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogDebug("Publishing event to RabbitMQ: {EventId}", @event.Id);

                    properties.CorrelationId = @event.CorrelationId;
                    if (isResponse)
                    {
                        replyQueueName = channel.QueueDeclare().QueueName;
                        _logger.LogDebug("ReplyQueueName: {replyQueueName}", replyQueueName);
                        properties.CorrelationId = guidId;
                        properties.ReplyTo = replyQueueName;
                    }
                    channel.BasicPublish(
                        exchange: RABITMQ_EXCHANGE,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
                if (isResponse)
                    SubscribeResponse(replyQueueName, guidId);
            }
        }
        private void DoInternalSubscription(string eventName)
        {
            _logger.LogDebug("DoInternalSubscription");
            _logger.LogDebug($"EVENT NAME - {eventName}");

            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            _logger.LogDebug($"Contains key ? - {containsKey}");

            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: RABITMQ_EXCHANGE,
                                      routingKey: eventName);
                }
            }
        }
        private void SubscribeResponse(string responseQueue, string correlationId)
        {
            _logger.LogDebug($"RESPONSE QUEUE {responseQueue}");
            _logger.LogDebug($"CORRELATIONID {correlationId}");

            DoInternalSubscription(responseQueue);
            _subsManager.AddResponseSubscription(responseQueue, correlationId);
            StartBasicConsume();
        }

        private void StartBasicConsume()
        {
            _logger.LogDebug("Starting RabbitMQ basic consume");
            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            _logger.LogDebug("Consumer_Received");

            var routingKey = eventArgs.RoutingKey;
            _logger.LogDebug($"ROUTING KEY {routingKey}");

            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            _logger.LogDebug($"MESSAGE {message}");

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(routingKey, message, eventArgs.BasicProperties);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogDebug("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: RABITMQ_EXCHANGE,
                                    type: _xchangeType);

            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };
            return channel;
        }

        private async Task ProcessEvent(string eventName, string message, IBasicProperties properties)
        {
            _logger.LogDebug("ProcessEvent");

            _logger.LogDebug($"Processing RabbitMQ event: {eventName} with Message {message}");

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        _logger.LogDebug("Subscription details for the event {eventName}: {subscription}", eventName, subscription);
                        
                        if (subscription.IsReponse)
                        {
                            var resData = new Dictionary<string, string>();
                            var handlers = _subsManager.GetHandlersForEvent(eventName);
                            string key = handlers.FirstOrDefault().CorrelationId;
                            if (properties.CorrelationId == key)
                            {
                                resData.Add(key, message);
                                _responseMessage.TryAdd(resData, TimeSpan.FromMilliseconds(1000));
                            }
                        }
                        else
                        {
                            var handler = scope.Resolve(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            try
                            {
                                var responseTask = (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });

                                await responseTask.ConfigureAwait(false);
                                var resultProperty = responseTask.GetType().GetProperty("Result");
                                var response =resultProperty.GetValue(responseTask);

                                if (!string.IsNullOrEmpty(properties.ReplyTo))
                                {
                                    var responseData = (IntegrationEvent)response;
                                    responseData.ReplayRoutingId = properties.ReplyTo;
                                    responseData.CorrelationId = properties.CorrelationId;
                                    Publish(responseData);
                                }
                            }
                            catch(Exception ex)
                            {
                                _logger.LogError("Error Resolving the IIntegrationEventHandler {ex}", ex);
                            }
                           
                        }

                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            }
        }
    }
    #endregion

}
