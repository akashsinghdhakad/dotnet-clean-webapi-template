using Microsoft.EntityFrameworkCore;
using dotnetWebApiCoreCBA.Models.Entities;

namespace dotnetWebApiCoreCBA.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<User> Users => Set<User>();


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: configure User constraints
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Username).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(50).IsRequired();
            });

            // You can also configure Todo here if needed
        }
    }
}
