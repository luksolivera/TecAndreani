
using Silverback.Messaging.Messages;

namespace BrokerCommon.Event
{
    public abstract class Event<T> : IEvent
    {
        public T Payload { get; set; }
        protected Event(T payload)
        {
            Payload = payload;
        }
    }
}
