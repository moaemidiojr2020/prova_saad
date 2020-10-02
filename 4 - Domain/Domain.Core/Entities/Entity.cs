using System;

namespace Domain.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id {get; protected set;}
    }
}