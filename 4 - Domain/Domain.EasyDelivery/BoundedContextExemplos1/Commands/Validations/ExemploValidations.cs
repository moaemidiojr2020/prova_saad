using System;
using FluentValidation;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Commands.Validations
{
    public abstract class ExemploValidations<T> : AbstractValidator<T> where T : ExemploBaseCommand
    {
          protected void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome deve ser preenchido.")
                .Length(2, 150).WithMessage("O Nome deve ter entre 2 a 150 caracteres.");
        }

        protected void ValidarDataNascimento()
        {
            RuleFor(c => c.DataNascimento)
                .NotEmpty()
                .Must(IdadeMinima)
                .WithMessage("É necessário ter 18 anos ou mais.");
        }

        protected void ValidarEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool IdadeMinima(DateTime dataNascimento)
        {
            return dataNascimento <= DateTime.Now.AddYears(-18);
        }
    }
}