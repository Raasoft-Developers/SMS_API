using EventBus.Events;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
         where TIntegrationEvent : IntegrationEvent

    {
        /// <summary>
        /// Handle the subscription event.
        /// </summary>
        /// <param name="event"><see cref="TIntegrationEvent"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<dynamic> Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
