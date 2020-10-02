using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Core.Repositories;
using Domain.SAAD.Prova.Enums;
using Domain.SAAD.Prova.Queries;
using Domain.SAAD.Prova.Repositories;
using Infra.Data.Core.ConnectionStrings;

namespace Infra.Data.ReadOnly.Repositories.Prova
{
	public class PessoaReadOnlyRepository : ReadOnlyRepository, IPessoaReadOnlyRepository
	{
		const string TABLE_PESSOA = "Pessoa";

		public PessoaReadOnlyRepository(ConnectionStringHandler connectionStringHandler) : base(connectionStringHandler)
		{
		}

		public async Task<IEnumerable<BuscaPessoaQuery>> BuscarPessoasAsync(string nome)
		{

			var sbSQL = new StringBuilder();
			var sql = $@"SELECT * FROM {SchemasConstants.ProvaContext}.{TABLE_PESSOA}";
			sbSQL.Append(sql);
			var dynamicParams = new DynamicParameters();

			if (!string.IsNullOrEmpty(nome))
			{
				var descricaoQuery = @" WHERE UPPER(Nome) LIKE '%' + UPPER(@p_nome) + '%'";
				sbSQL.Append(descricaoQuery);
				dynamicParams.Add("@p_nome", nome);
			}

			var data = await base.QueryAsync<BuscaPessoaQuery>(sbSQL.ToString(), dynamicParams);
			return data.OrderBy(c => c.nome);
		}

		public async Task<IEnumerable<string>> ObterEstadoCivilAsync()
		{
			List<string> tipos = new List<string>();

			foreach (var item in Enum.GetNames(typeof(EstadoCivilEnum)))
			{
				tipos.Add(item);
			}

			return tipos;
		}

		public async Task<BuscaPessoaQuery> ObterPessoaPorIdAsync(Guid Id)
		{
			var sql = $@"SELECT * FROM 
						{SchemasConstants.ProvaContext}.{TABLE_PESSOA} 
						WHERE Id = @p_id";
			var dynamicParams = new DynamicParameters();
			dynamicParams.Add("@p_id", Id);

			var data = await base.QueryAsync<BuscaPessoaQuery>(sql, dynamicParams);
			return data.FirstOrDefault();
		}
	}
}
