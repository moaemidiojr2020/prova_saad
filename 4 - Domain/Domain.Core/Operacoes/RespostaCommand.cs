using FluentValidation.Results;

namespace Domain.Core.Operacoes
{
    public class RespostaCommand
    {
        public ValidationResult ValidationResult { get; set; }
        public object Dados { get; set; }

        public bool IsValid => ValidationResult == null || ValidationResult.IsValid;

        public void AdicionarErro(string chave, string mensagem)
        {
            this.ValidationResult = ValidationResult ?? new ValidationResult();

            this.ValidationResult.Errors.Add(new ValidationFailure(chave, mensagem));
        }
    }
}