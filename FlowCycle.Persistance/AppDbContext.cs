using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Remove the EnsureCreated and Migrate calls from here
        }

        public DbSet<StockItemDao> Stocks { get; set; }
        public DbSet<ProjectDao> Projects { get; set; }
        public DbSet<CategoryDao> Categories { get; set; }
        public DbSet<SupplierDao> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockItemDao>(entity =>
            {
                entity.Property(e => e.VAT).HasComment("НДС");
                entity.Property(e => e.ReceiptDate).HasComment("Дата поступления");
            });
        }
    }
}
