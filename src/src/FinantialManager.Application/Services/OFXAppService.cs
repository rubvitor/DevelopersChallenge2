using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FinantialManager.Application.EventSourcedNormalizers;
using FinantialManager.Application.Interfaces;
using FinantialManager.Application.ViewModels;
using FinantialManager.Domain.Commands;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using NetDevPack.Mediator;

namespace FinantialManager.Application.Services
{
    public class OFXAppService : IOFXAppService
    {
        private readonly IMapper _mapper;
        private readonly IOFXRepository _OFXRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public OFXAppService(IMapper mapper,
                                  IOFXRepository OFXRepository,
                                  IMediatorHandler mediator,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _OFXRepository = OFXRepository;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IEnumerable<OFX>> GetAll()
        {
            return await _OFXRepository.GetAll();
        }

        public async Task<OFX> GetById(string id)
        {
            return await _OFXRepository.GetById(id);
        }

        public async Task<List<STMTTRN>> GetSTMTTRNByAccountId(string accountId)
        {
            return await _OFXRepository.GetSTMTTRNByAccountId(accountId);
        }

        public async Task<List<STMTTRN>> GetAllSTMTTRN()
        {
            return await _OFXRepository.GetAllSTMTTRN();
        }

        public async Task<ValidationResult> Register(OFXViewModel OFXViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewOFXCommand>(OFXViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(OFXViewModel OFXViewModel)
        {
            var updateCommand = _mapper.Map<UpdateOFXCommand>(OFXViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(string id)
        {
            var removeCommand = new RemoveOFXCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IList<OFXHistoryData>> GetAllHistory(string id)
        {
            return OFXHistory.ToJavaScriptOFXHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
