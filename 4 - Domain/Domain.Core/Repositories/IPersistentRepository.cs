using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.UnitOfWork;

namespace Domain.Core.Repositories
{
    public interface IPersistentRepository<T> : IRepository
    where T : Entity
    {
        IUoW UnitOfWork { get; }

        T Add(T obj);
        T Update(T obj);
        T Remove(T obj);
    }
}