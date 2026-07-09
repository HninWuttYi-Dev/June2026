using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace June2026.ConsoleApp2
{
    internal class DapperService
    {
        private readonly SqlConnectionStringBuilder _sb = new SqlConnectionStringBuilder
        {
            DataSource = ".",
            InitialCatalog = "June2026DB",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };
        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_sb.ConnectionString))
            {
                db.Open();
                List<Student> lst = db.Query<Student>("SELECT * FROM Student;").ToList();
                foreach (Student item in lst)
                {
                    DateTime dTime = Convert.ToDateTime(item.DateOfBirth);
                    Console.WriteLine(@$"
                    StudentNo: {item.StudentNo}
                    Name: {item.StudentName}
                    FatherName: {item.FatherName}
                    Email: {item.Email}
                    DateOfBirth: {dTime.ToString("dd-MMM-yyyy")}
                    ");
                    Console.WriteLine("===============================================");
                }
            }

        }
        public void Create()
        {
            using (IDbConnection db = new SqlConnection(_sb.ConnectionString))
            {
                db.Execute(@"INSERT INTO Student(StudentName, FatherName, StudentNo, Email, DateOfBirth)
                VALUES 
                ('Harry Potter', 'James Potter', 'STU-006', 'harry@gmail.com', '2000-08-12'),
                ('Ron Wealsey', 'Arthur Wealsey', 'STU-007', 'ron@gmail.com', '2000-09-12'),
                ('Hermione Granger', 'Mr.Granger', 'STU-008', 'hermione@gmail.com', '2000-08-12');");
            }
        }
        public void Update()
        {
            using (IDbConnection db = new SqlConnection(_sb.ConnectionString))
            {
                db.Execute(@"UPDATE Student
                    SET DateOfBirth = '1979-09-19'
                    WHERE StudentID = 1004");
            }
        }
        public void Delete()
        {
            using (IDbConnection db = new SqlConnection(_sb.ConnectionString))
            {
                db.Execute(@"DELETE Student
                WHERE StudentID IN(1002, 1003, 1004);");
            }
        }
        public class Student
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public string FatherName { get; set; }
            public string StudentNo { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
            public bool isDeleted { get; set; }
        }
    }
}