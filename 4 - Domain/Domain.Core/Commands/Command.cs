using FluentValidation.Results;
using MediatR;

namespace Domain.Core.Commands
{
    public abstract class Command : IRequest<ValidationResult>, IBaseRequest
    {
        public ValidationResult ValidationResult { get; set; }
        public virtual bool IsValid()
        {
            return true;
        }
    }
}