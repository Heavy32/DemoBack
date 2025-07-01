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

        public DbSet<StorageItemDao> Storages { get; set; }
        public DbSet<ProjectDao> Projects { get; set; }
        public DbSet<CategoryDao> Categories { get; set; }
        public DbSet<SupplierDao> Suppliers { get; set; }
        public DbSet<CostingDao> Costings { get; set; } = null!;
        public DbSet<CostingMaterialDao> CostingMaterials { get; set; }
        public DbSet<CostingLaborDao> CostingLabors { get; set; }
        public DbSet<CostingOverheadDao> CostingOverheads { get; set; } = null!;
        public DbSet<CostingTypeDao> CostingTypes { get; set; } = null!;
        public DbSet<OverheadTypeDao> OverheadTypes { get; set; } = null!;
        public DbSet<CostingMaterialTypeDao> CostingMaterialTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StorageItemDao>(entity =>
            {
                entity.Property(e => e.VAT).HasComment("НДС");
                entity.Property(e => e.ArrivalDate).HasComment("Дата поступления (Arrival Date)");
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

            // Seed CostingTypes
            modelBuilder.Entity<CostingTypeDao>().HasData(
                new CostingTypeDao { Id = 1, Name = "Прогнозный" },
                new CostingTypeDao { Id = 2, Name = "Фактический" },
                new CostingTypeDao { Id = 3, Name = "Плановый" }
            );

            modelBuilder.Entity<OverheadTypeDao>().HasData(
                new OverheadTypeDao { Id = 1, Name = "Прямой" },
                new OverheadTypeDao { Id = 2, Name = "Косвенный" }
            );

            // Seed CostingMaterialTypes
            modelBuilder.Entity<CostingMaterialTypeDao>().HasData(
                new CostingMaterialTypeDao { Id = 1, Name = "сырье" },
                new CostingMaterialTypeDao { Id = 2, Name = "полуфабрикаты" },
                new CostingMaterialTypeDao { Id = 3, Name = "оборудование" },
                new CostingMaterialTypeDao { Id = 4, Name = "комплектующие" },
                new CostingMaterialTypeDao { Id = 5, Name = "материал" }
            );
        }
    }
}
