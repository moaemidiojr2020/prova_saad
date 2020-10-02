using System;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Core.Entities
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {

        public Guid Id { get; protected set; }

        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            return ValidationResult != null && ValidationResult.IsValid;
        }
    }
}