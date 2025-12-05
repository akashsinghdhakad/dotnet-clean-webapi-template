using Microsoft.EntityFrameworkCore;
using DotnetWebApiCoreCBA.Models.Entities;

namespace DotnetWebApiCoreCBA.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos => Set<Todo>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
