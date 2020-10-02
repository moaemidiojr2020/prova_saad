using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Domain.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.ReadOnly.Repositories
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        public string _connString;

        public ReadOnlyRepository(IConfiguration configuration)
        {
            this._connString = configuration.GetConnectionString("DefaultConnection");
        }
        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return await WithConnectionAsync(async c =>
            {
                var data = await c.QueryAsync<T>(sql, param);

                return data;
            });
        }

        protected async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await WithConnectionAsync(async c =>
            {
                var data = await c.ExecuteAsync(sql, param);

                return data;
            });
        }


        private async Task<T> WithConnectionAsync<T>(Func<SqlConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(_connString))
                {
                    await connection.OpenAsync();

                    return await getData(connection);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<T> QueryAsync<T, U>(string sql, string splitOn,
         Func<T, U, T> mapFunction,
         Func<IEnumerable<T>, T> consolidationMap, object param = null)
        {
            var mappedData = await WithConnectionAsync(async c =>
            {
                var data = await c.QueryAsync<T, U, T>(sql, (t, u) =>
                {
                    return mapFunction(t, u);
                }, splitOn: splitOn, param: param);

                return data;
            });

            var data = consolidationMap(mappedData);

            return data;
        }

        protected async Task<IEnumerable<T>> QueryAsync<T, U>(string sql, string splitOn,
       Func<T, U, T> mapFunction,
       Func<IEnumerable<T>, IEnumerable<T>> consolidationMap, object param = null)
        {
            var mappedData = await WithConnectionAsync(async c =>
            {
                var data = await c.QueryAsync<T, U, T>(sql, (t, u) =>
                {
                    return mapFunction(t, u);
                }, splitOn: splitOn, param: param);

                return data;
            });

            var data = consolidationMap(mappedData);

            return data;
        }
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}