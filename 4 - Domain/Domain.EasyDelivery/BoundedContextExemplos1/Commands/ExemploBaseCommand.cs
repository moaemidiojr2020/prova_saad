using System;
using Domain.Core.Commands;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Commands
{
    public class ExemploBaseCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Nome { get; protected set; }

        public string Email { get; protected set; }

        public DateTime DataNascimento { get; protected set; }
    }
}