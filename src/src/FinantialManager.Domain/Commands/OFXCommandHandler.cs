using System;
using System.Threading;
using System.Threading.Tasks;
using FinantialManager.Domain.Events;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;

namespace FinantialManager.Domain.Commands
{
    public class OFXCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewOFXCommand, ValidationResult>,
        IRequestHandler<UpdateOFXCommand, ValidationResult>,
        IRequestHandler<RemoveOFXCommand, ValidationResult>
    {
        private readonly IOFXRepository _OFXRepository;
        private readonly IOFXBusiness _OFXBusiness;

        public OFXCommandHandler(IOFXRepository OFXRepository,
                                 IOFXBusiness OFXBusiness)
        {
            _OFXRepository = OFXRepository;
            _OFXBusiness = OFXBusiness;
        }

        public async Task<ValidationResult> Handle(RegisterNewOFXCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var OFX = message.OFX;

            if (await _OFXRepository.GetById(OFX.Id) != null)
            {
                AddError("The OFX has already been imported.");
                return ValidationResult;
            }

            var stmttrn = await _OFXBusiness.RemoveTranDuplicates(OFX);

            try
            {
                if (stmttrn != null && stmttrn.Count > 0)
                    _OFXRepository.AddSTMTTRNCollection(stmttrn);

                OFX.BANKMSGSRSV1 = null;
                OFX.SIGNONMSGSRSV1 = null;

                _OFXRepository.Add(OFX);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return ValidationResult;
            }

            return await Commit(_OFXRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateOFXCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var OFX = message.OFX;
            var existingOFX = await _OFXRepository.GetById(OFX.Id);

            if (existingOFX == null)
            {
                AddError("The OFX not exist.");
                return ValidationResult;
            }

            var stmttrn = await _OFXBusiness.RemoveTranDuplicates(OFX);

            try
            {
                if (stmttrn != null && stmttrn.Count > 0)
                    _OFXRepository.UpdateSTMTTRNCollection(stmttrn);

                OFX.BANKMSGSRSV1 = null;
                OFX.SIGNONMSGSRSV1 = null;

                _OFXRepository.Update(OFX);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return ValidationResult;
            }

            return await Commit(_OFXRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveOFXCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var OFX = await _OFXRepository.GetById(message.OFX.Id);

            if (OFX is null)
            {
                AddError("The OFX doesn't exists.");
                return ValidationResult;
            }

            try
            {
                var stmttrnList = await _OFXRepository.GetSTMTTRNByAccountId(message.OFX.AccountId);

                _OFXRepository.RemoveSTMTTRNCollection(stmttrnList);

                OFX.BANKMSGSRSV1 = null;
                OFX.SIGNONMSGSRSV1 = null;

                _OFXRepository.Remove(OFX);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                return ValidationResult;
            }

            return await Commit(_OFXRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _OFXRepository.Dispose();
        }
    }
}