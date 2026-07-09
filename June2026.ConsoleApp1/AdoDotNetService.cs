using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace June2026.ConsoleApp1
{
    public class AdoDotNetService
    {
        public void Read()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = ".";
            sb.InitialCatalog = "June2026DB";
            sb.UserID = "sa";
            sb.Password = "sasa@123";
            sb.TrustServerCertificate = true;
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"SELECT
                StudentId,
                StudentName,
                FatherName,
                StudentNo,
                Email,
                DateOfBirth,
                IsDeleted
            from Student;";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            adapter.Fill(dtable);
            connection.Close();
            foreach (DataRow item in dtable.Rows)
            {
                Console.WriteLine(item["StudentId"]);
                Console.WriteLine(item["StudentName"]);
               Console.WriteLine(item["FatherName"]);
                Console.WriteLine(item["StudentNo"]);
                Console.WriteLine(item["Email"]);
                DateTime dtime = Convert.ToDateTime(item["DateOfBirth"]);
                Console.WriteLine(dtime.ToString("dd-MMM-yyyy"));
                Console.WriteLine(item["Email"]);
                System.Console.WriteLine(item["IsDeleted"]);
                Console.WriteLine("============================");
            }
        }
        public void Create()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = ".";
            sb.InitialCatalog = "June2026DB";
            sb.UserID = "sa";
            sb.Password = "sasa@123";
            sb.TrustServerCertificate = true;
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"
                INSERT INTO Student (StudentName, FatherName, StudentNo, Email, DateOfBirth)
                VALUES 
                    ('Ko Ko', 'U Ba', 'STU-011', 'koko@example.com', '2002-01-01'),
                    ('Nilar', 'U Mya', 'STU-012', 'nilar@example.com', '2001-05-12'),
                    ('Zaw Zaw', 'U Hla', 'STU-013', 'zawzaw@example.com', '2000-11-20');
            ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
        public void Update()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = ".";
            sb.InitialCatalog = "June2026DB";
            sb.UserID = "sa";
            sb.Password = "sasa@123";
            sb.TrustServerCertificate = true;
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"UPDATE Student SET Email = 'nilar111@example.com' where StudentID = 3003;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = ".";
            sb.InitialCatalog = "June2026DB";
            sb.UserID = "sa";
            sb.Password = "sasa@123";
            sb.TrustServerCertificate = true;
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = "DELETE Student WHERE StudentID = 3004;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}