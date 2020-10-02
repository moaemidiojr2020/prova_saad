using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core.Repositories;
using Domain.SAAD.Prova.Queries;

namespace Domain.SAAD.Prova.Repositories
{
	public interface IPessoaReadOnlyRepository : IReadOnlyRepository
	{
		Task<IEnumerable<string>> ObterEstadoCivilAsync();
		Task<BuscaPessoaQuery> ObterPessoaPorIdAsync(Guid Id);
		Task<IEnumerable<BuscaPessoaQuery>> BuscarPessoasAsync(string nome);
	}
}
