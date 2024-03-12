using ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Helpers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

    }
}

