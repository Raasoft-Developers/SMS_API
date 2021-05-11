using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        /// <summary>
        /// Publish the message to messaging server. 
        /// </summary>
        /// <param name="event"></param>
        void Publish(IntegrationEvent @event);
        /// <summary>
        /// Publish the message to messaging server and return the response from the server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        T Publish<T>(IntegrationEvent @event);
        /// <summary>
        /// Subscribe an event for receving the message 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
     
    }
}
