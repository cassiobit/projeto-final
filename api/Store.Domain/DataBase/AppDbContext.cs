using System.IO;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Domain.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Directory.GetCurrentDirectory();
            optionsBuilder.UseSqlite(@$"DataSource={path}/database.db;Cache=Shared");
        }
    }
}