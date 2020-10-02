using System;
using Domain.EasyDelivery.BoundedContextExemplos1.Commands.Validations;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Commands
{
    public class AddExemploCommand : ExemploBaseCommand
    {
        public AddExemploCommand(string nome, string email, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddExemploCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}