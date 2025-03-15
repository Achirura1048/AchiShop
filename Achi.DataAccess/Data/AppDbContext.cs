using Microsoft.EntityFrameworkCore;
using E_Ticaret.Models;
namespace E_Ticaret.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { ID = 1, Name = "Action", DisplayOrder = 1 });
            modelBuilder.Entity<Category>().HasData(new Category { ID = 2, Name = "Sci-Fi", DisplayOrder =2 });
            modelBuilder.Entity<Category>().HasData(new Category { ID = 3, Name = "Fantasy", DisplayOrder = 3 });
        }
    }
}
