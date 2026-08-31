using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace June2026.CommonService
{
    public class AdoDotNetService
    {
        public void Read()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "June2026DB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
            };
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
            from Tbl_Student;";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dTbl = new DataTable();
            adapter.Fill(dTbl);
            connection.Close();
            foreach (DataRow item in dTbl.Rows)
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
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "June2026DB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate= true
            };
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"
                INSERT INTO Tbl_Student (StudentName, FatherName, StudentNo, Email, DateOfBirth)
                VALUES 
                    ('Hla Hla', 'U Ba', 'STU-011', 'koko@example.com', '2002-01-01'),
                    ('Ni Ni', 'U Mya', 'STU-012', 'nilar@example.com', '2001-05-12'),
                    ('Zaw Zaw', 'U Hla', 'STU-013', 'zawzaw@example.com', '2000-11-20');
            ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void Update()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "June2026DB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
            };
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"UPDATE Tbl_Student SET Email = 'nilar222@example.com' where StudentID = 3003;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "June2026DB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
            };
            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            connection.Open();
            string query = @"DELETE Tbl_Student where StudentID = 3003;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}