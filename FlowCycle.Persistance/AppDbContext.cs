using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<StockItemDao> Stocks { get; set; }
        public DbSet<ProjectDao> Projects { get; set; }
        public DbSet<CategoryDao> Categories { get; set; }
        public DbSet<SupplierDao> Suppliers { get; set; }
    }
}
