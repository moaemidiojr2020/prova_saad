using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core.Repositories;
using Domain.EasyDelivery.BoundedContextExemplos1.Queries;

namespace Domain.EasyDelivery.BoundedContextExemplos1.Repositories
{
    public interface IExemploReadOnlyRepository : IReadOnlyRepository
    {
         Task<IEnumerable<ExemploQuery>> ObterExemplosAsync();
         Task<IEnumerable<ExemploIdNomeQuery>> ObterExemploIdNomeAsync();

    }
}