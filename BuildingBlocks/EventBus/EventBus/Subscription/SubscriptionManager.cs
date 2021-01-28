using EventBus.Abstractions;
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EventBus.Subscription
{
    public class SubscriptionManager : ISubscriptionManager
    {

        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public SubscriptionManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }
        #region public methods
        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();
        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            DoAddSubscription(typeof(TH), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }
        public void AddResponseSubscription(string eventName, string correlationId)
        {
            DoAddSubscription(null, eventName, isDynamic: false, correlationId);
        }
        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);
        #endregion
        #region private methods
        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic, string correlationId = "")
        {
            bool isResponse = !string.IsNullOrEmpty(correlationId);
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                    throw new ArgumentException(
                        $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            if (isResponse)
                _handlers[eventName].Add(SubscriptionInfo.Response(handlerType, correlationId));
            else
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));

        }
        #endregion

    }
}
