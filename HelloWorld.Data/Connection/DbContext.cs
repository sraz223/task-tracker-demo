using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Data.Connection
{
    public interface IDbContext
    {
        Task<IEnumerable<T>> GetManyAsync<T>(string storedProcedureName, params SqlParameter[] parameters);
        void ExecuteAsync(string storedProcedureName, params SqlParameter[] parameters);
    }

    public class DbContext : IDbContext
    {
        private readonly IConnectionFactory _connectionFactory;

        public DbContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> GetManyAsync<T>(string storedProcedureName, params SqlParameter[] parameters)
        {
            if (storedProcedureName.Contains(" "))
                storedProcedureName = storedProcedureName.Substring(0, storedProcedureName.IndexOf(" "));

            using (var connection = _connectionFactory.GetConnection())
            {
                var p = new DynamicParameters();
                foreach (SqlParameter parameter in parameters)
                    p.Add(parameter.ParameterName, parameter.Value);

                return await connection.QueryAsync<T>(storedProcedureName, p, commandType: CommandType.StoredProcedure);
            }
        }

        public async void ExecuteAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            if (storedProcedureName.Contains(" "))
                storedProcedureName = storedProcedureName.Substring(0, storedProcedureName.IndexOf(" "));

            using (var connection = _connectionFactory.GetConnection())
            {
                var p = new DynamicParameters();
                foreach (SqlParameter parameter in parameters)
                    p.Add(parameter.ParameterName, parameter.Value);

                await connection.ExecuteAsync(storedProcedureName, p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
