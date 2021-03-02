using BrokerCommon.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerCommon.Event
{
    public class ConcreteEvent : Event<EventDto>
    {
        public ConcreteEvent(EventDto payload) : base(payload)
        {
        }
    }
}
