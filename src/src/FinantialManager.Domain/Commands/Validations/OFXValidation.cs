using FluentValidation;

namespace FinantialManager.Domain.Commands.Validations
{
    public abstract class OFXValidation<T> : AbstractValidator<T> where T : OFXCommand
    {
        protected void ValidateOFX()
        {
            RuleFor(c => c.OFX)
                .NotEmpty().WithMessage("Please ensure you have entered the OFX");
        }
    }
}