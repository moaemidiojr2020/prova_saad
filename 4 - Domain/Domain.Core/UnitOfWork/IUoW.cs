using System;
using System.Threading.Tasks;

namespace Domain.Core.UnitOfWork
{
    public interface IUoW
    {
        Task<bool> SaveChangesAsync();
         Task CommitAsync();
         Task BeginTransactionAsync();
    }
}