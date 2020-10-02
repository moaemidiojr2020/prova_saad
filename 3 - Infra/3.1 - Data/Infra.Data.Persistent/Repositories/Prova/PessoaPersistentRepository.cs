using Domain.Core.UnitOfWork;
using Domain.SAAD.Prova.Models;
using Domain.SAAD.Prova.Repositories;

namespace Infra.Data.Persistent.Repositories.Prova
{
	public class PessoaPersistentRepository : PersistentRepository<PessoaRoot>, IPessoaPersistentRepository
	{
		public PessoaPersistentRepository(IUoW uow) : base(uow)
		{
		}
	}
}
