using Domain.Core.Operacoes;
using FluentValidation.Results;
using MediatR;

namespace Domain.Core.Commands
{
    public abstract class Command : IRequest<RespostaCommand>, IBaseRequest
    {
    }
}