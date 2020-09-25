using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.CrossCutting.Util.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialManager.Services.Business
{
    public class OFXBusiness : IOFXBusiness
    {
        private readonly IOFXRepository _OFXRepository;
        public OFXBusiness(IOFXRepository OFXRepository)
        {
            _OFXRepository = OFXRepository;
        }

        public async Task<List<STMTTRN>> RemoveTranDuplicates(OFX ofx)
        {
            var STMTTRNBase = new List<STMTTRN>();

            foreach (var stm in ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.STMTTRN)
            {
                var id = stm.Id;
                if (id == null || id == string.Empty)
                {
                    var inputStm = $"{stm.TRNTYPE.ToUpper().Replace(" ", string.Empty)}" +
                                   $"{stm.DTPOSTED.ToUpper().Replace(" ", string.Empty)}" +
                                   $"{stm.TRNAMT}" +
                                   $"{stm.MEMO.ToUpper().Replace(" ", string.Empty)}";

                    id = inputStm.GetHash();
                }

                if (await _OFXRepository.GetTransactionById(id, ofx.AccountId) == null 
                    && !STMTTRNBase.Any(s => s.Id == id && s.AccountId == ofx.AccountId))
                {
                    STMTTRNBase.Add(new STMTTRN
                    {
                        AccountId = ofx.AccountId,
                        Id = id,
                        DTPOSTED = stm.DTPOSTED,
                        MEMO = stm.MEMO,
                        OFXId = ofx.Id,
                        TRNAMT = stm.TRNAMT,
                        TRNTYPE = stm.TRNTYPE
                    });
                }
            }

            return STMTTRNBase;
        }
    }
}
