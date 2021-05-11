using System;

namespace EventBus.Subscription
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; set; }
        public Type EventType { get; set; }
        public bool IsDynamic { get; }
        public string CorrelationId { get; set; }
        public bool IsReponse { get; set; }

        private SubscriptionInfo(bool isDynamic, Type handlerType,bool isResponse = false , string correlationId = "")
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
            CorrelationId = correlationId;
            IsReponse = isResponse;
        }
        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
        public static SubscriptionInfo Response(Type handlerType,string correlationId)
        {
            return new SubscriptionInfo(false, handlerType, true, correlationId);
        }
    }
}
