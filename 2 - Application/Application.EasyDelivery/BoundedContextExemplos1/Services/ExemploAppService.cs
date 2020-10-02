using System.Threading.Tasks;
using Application.EasyDelivery.Core;
using Application.EasyDelivery.BoundedContextExemplos1.ViewModels;
using AutoMapper;
using Domain.EasyDelivery.BoundedContextExemplos1.Commands;
using FluentValidation.Results;
using MediatR;

namespace Application.EasyDelivery.BoundedContextExemplos1.Services
{
    public interface IExemploAppService : IBaseAppService
    {
        Task<ValidationResult> AddAsync(AddExemploVM viewModel);
    }
    
    public class ExemploAppService : BaseAppService, IExemploAppService
    {
        public ExemploAppService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<ValidationResult> AddAsync(AddExemploVM viewModel)
        {
            var cmd = base.mapper.Map<AddExemploCommand>(viewModel);

            return await base.mediator.Send(cmd);
        }
    }
}