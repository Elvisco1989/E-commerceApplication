using Microsoft.EntityFrameworkCore;

namespace E_commerceApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Food> Foods { get; set; }
      
    }
    
    
}
