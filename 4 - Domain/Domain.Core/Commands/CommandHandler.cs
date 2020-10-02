using FluentValidation.Results;

namespace Domain.Core.Commands
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult {get; set;}
        
    }
}