namespace FinantialManager.Domain.Commands.Validations
{
    public class UpdateOFXCommandValidation : OFXValidation<UpdateOFXCommand>
    {
        public UpdateOFXCommandValidation()
        {
            ValidateOFX();
        }
    }
}