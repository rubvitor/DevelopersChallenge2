using System.Collections.Generic;
using System.Threading.Tasks;
using FinantialManager.Domain.Models;
using NetDevPack.Data;

namespace FinantialManager.Domain.Interfaces
{
    public interface IOFXRepository : IRepository<OFX>
    {
        Task<OFX> GetById(string id);
        Task<OFX> GetByBankId(ushort bankId);
        Task<IEnumerable<OFX>> GetAll();

        void Add(OFX OFX);
        void Update(OFX OFX);
        void Remove(OFX OFX);
        Task<STMTTRN> GetTransactionById(string tranId, string accountId);
        void AddSTMTTRN(STMTTRN STMTTRN);
        void UpdateSTMTTRN(STMTTRN STMTTRN);
        void AddSTMTTRNCollection(List<STMTTRN> STMTTRN);
        void UpdateSTMTTRNCollection(List<STMTTRN> STMTTRN);
        void RemoveSTMTTRN(STMTTRN STMTTRN);
        void RemoveSTMTTRNCollection(List<STMTTRN> STMTTRN);
        Task<List<STMTTRN>> GetSTMTTRNByAccountId(string accoundtId);
        Task<List<STMTTRN>> GetAllSTMTTRN();
    }
}