using System;
using NetDevPack.Messaging;

namespace FinantialManager.Domain.Events
{
    public class OFXUpdatedEvent : Event
    {
        public string Id { get; set; }
        public OFXUpdatedEvent(string id)
        {
            Id = id;
        }
    }
}