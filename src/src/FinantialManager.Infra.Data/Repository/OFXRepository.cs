using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.ODataLinq.Helpers;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace FinantialManager.Infra.Data.Repository
{
    public class OFXRepository : IOFXRepository
    {
        protected readonly FinantialManagerContext Db;
        protected readonly DbSet<OFX> DbSet;
        protected readonly DbSet<STMTTRN> DbSetSTM;

        public OFXRepository(FinantialManagerContext context)
        {
            Db = context;
            DbSet = Db.Set<OFX>();
            DbSetSTM = Db.Set<STMTTRN>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<OFX> GetById(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<OFX>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<OFX> GetByBankId(ushort bankId)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.BANKID == bankId);
        }

        public async Task<List<STMTTRN>> GetSTMTTRNByAccountId(string accoundtId)
        {
            return await DbSetSTM.AsNoTracking().Where(s => s.AccountId == accoundtId).ToListAsync();
        }

        public async Task<List<STMTTRN>> GetAllSTMTTRN()
        {
            return await DbSetSTM.AsNoTracking().ToListAsync();
        }

        public async Task<STMTTRN> GetTransactionById(string tranId, string accountId)
        {
            try
            {
                return await DbSetSTM.AsNoTracking().FirstOrDefaultAsync(c => c.Id == tranId && c.AccountId == accountId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddSTMTTRNCollection(List<STMTTRN> STMTTRN)
        {
            DbSetSTM.AddRange(STMTTRN);
        }

        public void AddSTMTTRN(STMTTRN STMTTRN)
        {
            DbSetSTM.Add(STMTTRN);
        }

        public void UpdateSTMTTRN(STMTTRN STMTTRN)
        {
            DbSetSTM.Update(STMTTRN);
        }

        public void UpdateSTMTTRNCollection(List<STMTTRN> STMTTRN)
        {
            DbSetSTM.UpdateRange(STMTTRN);
        }

        public void Add(OFX OFX)
        {
           DbSet.Add(OFX);
        }

        public void Update(OFX OFX)
        {
            DbSet.Update(OFX);
        }

        public void RemoveSTMTTRN(STMTTRN STMTTRN)
        {
            DbSetSTM.Remove(STMTTRN);
        }

        public void RemoveSTMTTRNCollection(List<STMTTRN> STMTTRN)
        {
            DbSetSTM.RemoveRange(STMTTRN);
        }

        public void Remove(OFX OFX)
        {
            DbSet.Remove(OFX);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
