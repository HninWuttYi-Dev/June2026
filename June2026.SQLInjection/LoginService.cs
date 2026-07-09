using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace June2026.SQLInjection
{
    internal class LoginService
    {
        SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
          DataSource = ".",
          InitialCatalog = "June2026DB",
          UserID =  "sa", 
          Password = "sasa@123",
          TrustServerCertificate = true  
        };
        public void Login(string username, string password)
        {
           using(IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                var user = db.Query($"Select * From Tbl_User Where Username = @Username and Password = @Password", new
                {
                    Username= username,
                    Password = password
                }).FirstOrDefault();
                if(user != null)
                {
                    Console.WriteLine("Login Successful");
                }
                else
                {
                    Console.WriteLine("Username and password required");
                }
            }
        }
    }
}