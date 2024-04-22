﻿using Microsoft.Data.SqlClient;

namespace Acmepay_ltd.Services
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
