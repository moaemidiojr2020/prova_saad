namespace Domain.EasyDelivery.BoundedContextExemplos1.Commands.Validations
{
    public class AddExemploCommandValidation : ExemploValidations<AddExemploCommand>
    {
        public AddExemploCommandValidation()
        {
            base.ValidarNome();
            base.ValidarEmail();
            base.ValidarDataNascimento();
        }
    }
}