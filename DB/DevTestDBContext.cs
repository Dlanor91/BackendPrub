using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class DevTestDBContext : DbContext
    {
        public DevTestDBContext(DbContextOptions<DevTestDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Stock>().ToTable("Stock");
        }

    }
}