using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlowCycle.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<StockItemDao> Stocks { get; set; }
        public DbSet<ProjectDao> Projects { get; set; }
        public DbSet<CategoryDao> Categories { get; set; }
        public DbSet<SupplierDao> Suppliers { get; set; }
        public DbSet<CostingDao> Costings { get; set; } = null!;
        public DbSet<CostingMaterialDao> CostingMaterials { get; set; }
        public DbSet<CostingLaborDao> CostingLabors { get; set; }
        public DbSet<CostingOverheadDao> CostingOverheads { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockItemDao>(entity =>
            {
                entity.Property(e => e.VAT).HasComment("НДС");
                entity.Property(e => e.ReceiptDate).HasComment("Дата поступления");
            });

            // Seed Categories
            modelBuilder.Entity<CategoryDao>().HasData(
                new CategoryDao { Id = 1, Name = "Уплотнительные материалы" },
                new CategoryDao { Id = 2, Name = "Крепеж и метизы" },
                new CategoryDao { Id = 3, Name = "Металлические изделия" },
                new CategoryDao { Id = 4, Name = "Машины и оборудование" }
            );

            // Seed Suppliers
            modelBuilder.Entity<SupplierDao>().HasData(
                new SupplierDao { Id = 1, Name = "ООО «СИЛУР»" },
                new SupplierDao { Id = 2, Name = "ООО «ТМЗ»" },
                new SupplierDao { Id = 3, Name = "ООО «МеталлИнвестПродукт»" },
                new SupplierDao { Id = 4, Name = "PVZ Ltd" }
            );

            // Seed Projects
            modelBuilder.Entity<ProjectDao>().HasData(
                new ProjectDao { Id = 1, Name = "ЮНГ 0035" },
                new ProjectDao { Id = 2, Name = "GMR 012 J" },
                new ProjectDao { Id = 3, Name = "-" }
            );
        }
    }
}
