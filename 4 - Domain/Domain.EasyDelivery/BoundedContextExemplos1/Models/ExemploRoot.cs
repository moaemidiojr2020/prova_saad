using System;
using Domain.Core.Entities;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Models
{
    //Este objeto represeta a entidade Exemplo em um BoundedContextExemplos1
    //É utilizado o pattern AggregateRoot pois é responsável por ser 
    //o objeto que fornece detalhes sobre Exemplo neste contexto e
    //seus objetos filhos neste mesmo contexto
    public class ExemploRoot : Entity
    {
        public ExemploRoot(Guid id, string nome, string email, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        //Vazio Para EntityFramework
        protected ExemploRoot()
        {
        }
        
        public string Nome { get; private set; }

        public string Email { get; private set; }

        public DateTime DataNascimento { get; private set; }
    }
}