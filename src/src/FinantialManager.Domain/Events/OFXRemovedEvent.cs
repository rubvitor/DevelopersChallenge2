using System;
using NetDevPack.Messaging;

namespace FinantialManager.Domain.Events
{
    public class OFXRemovedEvent : Event
    {
        public string AggregateId { get; set; }
        public OFXRemovedEvent(string id)
        {
            Id = id;
            AggregateId = id;
        }

        public string Id { get; set; }
    }
}