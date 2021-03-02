using Dapper;
using Domain.Interface.Queries;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistance.Queries
{
    public class GenericsQuery : IGenericsQuery
    {
        protected readonly IDbConnection _connection;
        public GenericsQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Exist(string table, string column, string[] values = null)
        {
            var sql = $@"
                    EXISTS(
                        SELECT *
                        FROM @table
                        WHERE @culumn in @values
                    );";
            return await _connection.QueryFirstAsync<bool>(sql, new { table, column, values });
        }

        public async Task<List<T>> GetAll<T>(string table) where T : class
        {
            var sql = $@"
                    SELECT *
                    FROM @table;
                    ";
            return (await _connection.QueryAsync<T>(sql, new { table })).AsList();
        }

        public async Task<T> GetById<T>(string table, string column, string id) where T : class
        {
            var sql = $@"
                    SELECT *
                    FROM @table
                    WHERE @culumn = @id
                    ";
            return await _connection.QueryFirstAsync<T>(sql, new { table, column, id });
        }
    }
}
