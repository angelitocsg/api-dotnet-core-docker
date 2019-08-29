using DotNetCoreDocker.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreDocker.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}