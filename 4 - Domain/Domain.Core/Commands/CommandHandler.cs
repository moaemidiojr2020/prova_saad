using Domain.Core.Operacoes;
using FluentValidation.Results;

namespace Domain.Core.Commands
{
    public class CommandHandler
    {
        public CommandHandler()
        {
            RespostaCommand = new RespostaCommand();
        }
        protected RespostaCommand RespostaCommand { get; set; }

        protected bool IsValid => RespostaCommand.ValidationResult.IsValid;

        protected void AdicionarErro(string chave, string mensagem)
        {
            this.RespostaCommand.ValidationResult = this.RespostaCommand.ValidationResult ?? new ValidationResult();
            this.RespostaCommand.ValidationResult.Errors.Add(new ValidationFailure(chave, mensagem));
        }
    }
}