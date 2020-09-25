namespace FinantialManager.Domain.Commands.Validations
{
    public class RegisterNewOFXCommandValidation : OFXValidation<RegisterNewOFXCommand>
    {
        public RegisterNewOFXCommandValidation()
        {
            ValidateOFX();
        }
    }
}