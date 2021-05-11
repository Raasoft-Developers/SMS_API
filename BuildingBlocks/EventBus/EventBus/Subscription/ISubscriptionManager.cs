using EventBus.Abstractions;
using EventBus.Events;
using System;
using System.Collections.Generic;

namespace EventBus.Subscription
{
    public interface ISubscriptionManager
    {
        void AddSubscription<T, TH>()
         where T : IntegrationEvent
         where TH : IIntegrationEventHandler<T>;
             string GetEventKey<T>();
        void AddResponseSubscription(string eventName, string correlationId);
        bool HasSubscriptionsForEvent(string eventName);
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
    }
}
