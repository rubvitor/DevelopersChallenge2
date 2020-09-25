using System;
using FinantialManager.Domain.Models;
using NetDevPack.Messaging;

namespace FinantialManager.Domain.Commands
{
    public abstract class OFXCommand : Command
    {
        public OFX OFX { get; protected set; }
    }
}