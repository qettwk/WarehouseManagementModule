using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
    public class WarehouseDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public DbSet<Automobile> Automobiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecordWrite> RecordWrites { get; set; }

        // разные schemas
        // прописывать ли вручную связи?


        public WarehouseDbContext(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("WarehouseManagment");
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Automobiles)
                .WithMany(a => a.Categories);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Automobiles)
                .WithMany(a => a.Categories);

            modelBuilder.Entity<Automobile>()
            .ToTable("Automobiles", "WarehouseManagment");
            modelBuilder.Entity<Category>()
            .ToTable("Categories", "WarehouseManagment");
            modelBuilder.Entity<RecordWrite>()
            .ToTable("RecordWrites", "WarehouseManagment");
        }

    }
}
