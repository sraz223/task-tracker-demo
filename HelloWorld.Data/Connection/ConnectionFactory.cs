using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HelloWorld.Data.Connection
{
    public interface IConnectionFactory
    {
        SqlConnection GetConnection();
        SqlConnection GetConnection(int timeout);
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlConnection GetConnection(int timeout)
        {
            return new SqlConnection(_connectionString + ";Connection Timeout=" + timeout);
        }
    }
}
