using System;
using NetDevPack.Messaging;

namespace FinantialManager.Domain.Events
{
    public class OFXRegisteredEvent : Event
    {
        public OFXRegisteredEvent(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}