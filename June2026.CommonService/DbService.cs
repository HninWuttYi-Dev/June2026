using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace June2026.CommonService
{
    public class DbService
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DbService(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public DataTable Query(string query, List<SqlParameterDto>? parameters = null )
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new(query, connection);
            if(parameters is not null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            SqlDataAdapter adapter = new(cmd);
            DataTable dTbl = new ();
            adapter.Fill(dTbl);
            connection.Close();
            return dTbl;
        }
        public int Execute(string query, List<SqlParameterDto>? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameters is not null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public class SqlParameterDto
        {
            public string Name {get; set;}
            public object Value {get; set;}
        }
    }
}