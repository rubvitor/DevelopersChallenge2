namespace FinantialManager.Domain.Commands.Validations
{
    public class RemoveOFXCommandValidation : OFXValidation<RemoveOFXCommand>
    {
        public RemoveOFXCommandValidation()
        {
            ValidateOFX();
        }
    }
}