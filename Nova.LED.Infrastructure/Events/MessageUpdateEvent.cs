using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Infrastructure.Events
{
    public class MessageUpdateEvent : PubSubEvent<MessageUpdateEvent>  //CompositePresentationEvent<MessageUpdateEvent>
    {
        public string Message { get; set; }
    }
}
