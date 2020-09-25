using System;
using FinantialManager.Domain.Commands.Validations;

namespace FinantialManager.Domain.Commands
{
    public class RemoveOFXCommand : OFXCommand
    {
        public string AggregateId { get; set; }
        public RemoveOFXCommand(string id)
        {
            OFX = new Models.OFX();
            OFX.Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOFXCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}