using Microsoft.EntityFrameworkCore;

namespace DemoBack
{
    public class AppDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class Record
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
