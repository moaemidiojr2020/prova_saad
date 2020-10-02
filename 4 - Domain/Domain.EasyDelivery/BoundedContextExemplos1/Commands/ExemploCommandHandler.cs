using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Commands;
using Domain.EasyDelivery.BoundedContextExemplos1.Models;
using Domain.EasyDelivery.BoundedContextExemplos1.Repositories;
using FluentValidation.Results;
using MediatR;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Commands
{
    public class ExemploCommandHandler : CommandHandler,
        IRequestHandler<AddExemploCommand, ValidationResult>
    {
        private readonly IExemploPersistentRepository _exemploPersistentRepository;

        public ExemploCommandHandler(IExemploPersistentRepository exemploPersistentRepository)
        {
            _exemploPersistentRepository = exemploPersistentRepository;
        }

        public async Task<ValidationResult> Handle(AddExemploCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid()) return request.ValidationResult;

                var exemplo = new ExemploRoot(Guid.NewGuid(), request.Nome, request.Email, request.DataNascimento);

                // iniciar transação
                await _exemploPersistentRepository.UnitOfWork.BeginTransactionAsync();

                // adicionar repositorio
                _exemploPersistentRepository.Add(exemplo);

                //se houver uma transação aberta, posso utilizar SaveChangesAsync
                //para obter as alterações antes de commitar/enviar para o banco
                // await _exemploPersistentRepository.UnitOfWork.SaveChangesAsync();

                // commitar transação
                await _exemploPersistentRepository.UnitOfWork.CommitAsync();

                return request.ValidationResult;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}