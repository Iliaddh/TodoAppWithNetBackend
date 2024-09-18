
using Microsoft.EntityFrameworkCore;
using TodoAppWithDotNet.Models;
namespace TodoAppWithDotNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
