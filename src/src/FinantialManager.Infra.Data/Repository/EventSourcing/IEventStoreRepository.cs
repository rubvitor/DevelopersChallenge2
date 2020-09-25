using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinantialManager.Domain.Core.Events;

namespace FinantialManager.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(string aggregateId);
    }
}