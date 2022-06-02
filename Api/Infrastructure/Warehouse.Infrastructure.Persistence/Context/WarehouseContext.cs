using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Warehouse.Api.Domain.Models;

namespace Warehouse.Infrastructure.Persistence.Context
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public string DbPath { get; }

        public WarehouseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "warehouse.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles", "test");
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
