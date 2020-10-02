using System;
using AutoMapper;
using MediatR;

namespace Application.EasyDelivery.Core
{
    public interface IBaseAppService : IDisposable
    {
        
    }
    public class BaseAppService : IBaseAppService
    {
        protected readonly IMediator mediator;
        protected readonly IMapper mapper;

        public BaseAppService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}