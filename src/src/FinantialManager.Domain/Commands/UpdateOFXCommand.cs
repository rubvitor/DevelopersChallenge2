using System;
using FinantialManager.Domain.Commands.Validations;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.CrossCutting.Util.Extensions;

namespace FinantialManager.Domain.Commands
{
    public class UpdateOFXCommand : OFXCommand
    {
        public UpdateOFXCommand(OFX OFX)
        {
            base.OFX = OFX;

            if (OFX.Id == null || OFX.Id == string.Empty)
                base.OFX.Id = OFX.GenerateOFXId();

            if (OFX.AccountId == null || OFX.AccountId == string.Empty)
            {
                base.OFX.AccountId = OFX.GenerateAccountId();
                base.OFX.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.Id = base.OFX.AccountId;
            }

            base.OFX.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.Id = base.OFX.AccountId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOFXCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}