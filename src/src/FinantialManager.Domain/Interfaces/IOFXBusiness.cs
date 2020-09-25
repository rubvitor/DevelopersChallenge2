using FinantialManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinantialManager.Domain.Interfaces
{
    public interface IOFXBusiness
    {
        Task<List<STMTTRN>> RemoveTranDuplicates(OFX ofx);
    }
}
