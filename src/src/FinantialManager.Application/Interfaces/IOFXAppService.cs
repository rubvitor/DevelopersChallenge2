using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinantialManager.Application.EventSourcedNormalizers;
using FinantialManager.Application.ViewModels;
using FinantialManager.Domain.Models;
using FluentValidation.Results;

namespace FinantialManager.Application.Interfaces
{
    public interface IOFXAppService : IDisposable
    {
        Task<IEnumerable<OFX>> GetAll();
        Task<OFX> GetById(string id);
        Task<ValidationResult> Register(OFXViewModel OFXViewModel);
        Task<ValidationResult> Update(OFXViewModel OFXViewModel);
        Task<ValidationResult> Remove(string id);
        Task<IList<OFXHistoryData>> GetAllHistory(string id);
        Task<List<STMTTRN>> GetSTMTTRNByAccountId(string accountId);
        Task<List<STMTTRN>> GetAllSTMTTRN();
    }
}
