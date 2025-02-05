using Microsoft.EntityFrameworkCore;
using DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Configuration;

using System.IO;


namespace DeveloperEvaluation.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Sale>()
                .Property(s => s.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .HasKey(si => new { si.ProductId, si.Quantity });

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.Discount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.Total)
                .HasComputedColumnSql("\"UnitPrice\" * \"Quantity\" - \"Discount\"", stored: true);

                        base.OnModelCreating(modelBuilder);

        }
    }
}
