using System;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Queries
{
    public struct ExemploQuery
    {
        public Guid id;
        public string nome;
        public string email;
        public string dataNascimento;

        public ExemploQuery(Guid id, string nome, string email, DateTime dataNascimento)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.dataNascimento = dataNascimento.ToShortDateString();
        }
    }
}