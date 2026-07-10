using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace June2026.EntityFrameworkCore
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder()
                {
                    DataSource = ".",
                    InitialCatalog = "June2026DB",
                    UserID = "sa",
                    Password = "sasa@123",
                    TrustServerCertificate = true
                };
                optionsBuilder.UseSqlServer(sb.ConnectionString);
            }
        }
        public DbSet<UserEntity> Users {get; set;}
        
    }
}