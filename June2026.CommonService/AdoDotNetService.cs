using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static June2026.CommonService.DbService;

namespace June2026.CommonService
{
    public class AdoDotNetService
    {
        public readonly DbService _dbService;

        public AdoDotNetService()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "June2026DB",
                UserID = "sa",
                Password = "sasa@123",
                TrustServerCertificate = true
            };

            _dbService = new DbService(sb);
        }

        public void Read()
        {
            string query = @"SELECT
                StudentId,
                StudentName,
                FatherName,
                StudentNo,
                Email,
                DateOfBirth,
                IsDeleted
            from Tbl_Student;";
            DataTable dt = _dbService.Query(query);
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine(item["StudentId"]);
                Console.WriteLine(item["StudentName"]);
                Console.WriteLine(item["FatherName"]);
                Console.WriteLine(item["StudentNo"]);
                Console.WriteLine(item["Email"]);
                DateTime dtime = Convert.ToDateTime(item["DateOfBirth"]);
                Console.WriteLine(dtime.ToString("dd-MMM-yyyy"));
                Console.WriteLine(item["IsDeleted"]);
                Console.WriteLine("============================");
            }

        }
        public void Create()
        {

            string query = @"
        INSERT INTO Tbl_Student (StudentName, FatherName, StudentNo, Email, DateOfBirth, isDeleted)
        VALUES (@StudentName, @FatherName, @StudentNo, @Email, @DateOfBirth, @isDeleted);";

            List<SqlParameterDto> parameters = new List<SqlParameterDto>
    {
        new() { Name = "StudentName", Value = "Sandar Win" },
        new() { Name = "FatherName", Value = "U Ba Ba" },
        new() { Name = "StudentNo", Value = "STU-16" },
        new() { Name = "Email", Value = "sandarwin@gmail.com" },
        new() { Name = "DateOfBirth", Value = new DateTime(2000, 11, 20) },
        new() { Name = "isDeleted", Value = false }

    };
            int result = _dbService.Execute(query, parameters);
            Console.WriteLine($"{result} row inserted");
        }
        public void Update()
        {
            string query = @"
            UPDATE Tbl_Student
            SET StudentName = @StudentName,
                FatherName =  @FatherName,
                StudentNo =  @StudentNo,
                Email = @Email,
                DateOfBirth = @DateOfBirth
            Where StudentId = @StudentId;
            ";
            List<SqlParameterDto> parameters = new List<SqlParameterDto>
        {
            new() { Name = "StudentID", Value = 2},
            new() { Name = "StudentName", Value = "Updated Name" },
            new() { Name = "FatherName", Value = "Updated Father" },
            new() { Name = "StudentNo", Value = "Updated StudentNo" },
            new() { Name = "Email", Value = "updated@gmail.com" },
            new() { Name = "DateOfBirth", Value = new DateTime(2000, 1, 1) },
        };
            int result = _dbService.Execute(query, parameters);
            Console.WriteLine($"{result} row updated");
        }
        public void Delete()
        {
            string query = @"DELETE Tbl_Student where StudentID = @StudentID;";
            var parameters =  new List<SqlParameterDto>
            {
                new() {Name = "StudentID", Value = 2}
            };
            int result = _dbService.Execute(query, parameters);
            Console.WriteLine($"{result} row deleted");
        }
    }
}