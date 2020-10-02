using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core.Repositories;
using Domain.EasyDelivery.BoundedContextExemplos1.Queries;
using Domain.EasyDelivery.BoundedContextExemplos1.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.ReadOnly.Repositories.BoundedContextExemplos1
{
    public class ExemploReadOnlyRepository : ReadOnlyRepository, IExemploReadOnlyRepository
    {
        const string TABLE = "Exemplos";

        public ExemploReadOnlyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<ExemploIdNomeQuery>> ObterExemploIdNomeAsync()
        {
            try
            {
                var sql = $@"SELECT Id, Nome FROM {SchemasConstants.EASY_SCHEMA}.{TABLE}";

                var data = await base.QueryAsync<ExemploIdNomeQuery>(sql);

                return data;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ExemploQuery>> ObterExemplosAsync()
        {
            try
            {
                var sql = $@"SELECT * FROM {SchemasConstants.EASY_SCHEMA}.{TABLE}";

                var data = await base.QueryAsync<ExemploQuery>(sql);

                return data;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}