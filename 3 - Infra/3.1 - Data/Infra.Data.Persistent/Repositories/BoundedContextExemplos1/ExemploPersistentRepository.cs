using Domain.Core.Repositories;
using Domain.EasyDelivery.BoundedContextExemplos1.Models;
using Domain.EasyDelivery.BoundedContextExemplos1.Repositories;
using Infra.Data.Persistent.Contexts;

namespace Infra.Data.Persistent.Repositories.BoundedContextExemplos1
{
  
    public class ExemploPersistentRepository : PersistentRepository<ExemploRoot>, IExemploPersistentRepository
    {
        public ExemploPersistentRepository(EasyDbContext context) : base(context)
        {
        }
    }
}