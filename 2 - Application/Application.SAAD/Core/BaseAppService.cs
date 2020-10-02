using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.UnitOfWork;
using MediatR;

namespace Application.SAAD.Core
{
    public interface IBaseAppService : IDisposable
    {

    }
    public class BaseAppService : IBaseAppService
    {
        protected readonly IMediator mediator;
        protected readonly IMapper mapper;

        protected IUoW UnitOfWork { get; }

        public BaseAppService(IMediator mediator, IMapper mapper,
        IUoW uow)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            UnitOfWork = uow;
        }

        protected async Task<System.Exception> ThrowExceptionAsync(System.Exception e)
        {
            await UnitOfWork.RollBackAsync();
            throw e;
        }
        
        public virtual void Dispose()
        {
            this.UnitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}