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
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { ID = 1, Name = "Action", DisplayOrder = 1 });
            modelBuilder.Entity<Category>().HasData(new Category { ID = 2, Name = "Sci-Fi", DisplayOrder =2 });
            modelBuilder.Entity<Category>().HasData(new Category { ID = 3, Name = "Fantasy", DisplayOrder = 3 });
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ID = 1,
                Title = "Fahrenheit 451",
                Description = "Kitapların yasaklandığı ve itfaiyecilerin onları yakmakla görevli olduğu bir toplumda, Guy Montag bu düzeni sorgulamaya başlar. Gerçeği arayışa çıktıkça, özgürlüğün ve bilginin gerçek değerini keşfeder.",
                ISBN = "9783060311354",
                Author = "Ray Bradbury",
                ListPrice = 9.99,
                Price = 8.99,
                Price50 = 8.49,
                Price100 = 7.99,
                //CategoryID = 3
            },

            new Product
            {
                ID = 2,
                Title = "Roger Ackroyd Cinayeti",
                Description = "Roger Ackroyd, aldığı bir mektupla büyük bir sırrı öğrenmesinin ardından evinde ölü bulunur. Emekli dedektif Hercule Poirot, kasabanın derinlerine gizlenmiş sırları açığa çıkararak katili bulmaya çalışır.",
                ISBN = "9780006167921",
                Author = "Agatha Christie",
                ListPrice = 15.99,
                Price = 14.99,
                Price50 = 14.49,
                Price100 = 13.99,
                //CategoryID = 3
            },

            new Product
            {
                ID = 3,
                Title = "Otomatik Portakal",
                Description = "Şiddet ve suçla dolu bir gelecekte, genç Alex ve çetesi acımasızca eğlenirken, yakalandığında devletin onu “iyileştirmek” için uyguladığı deneysel bir yönteme maruz kalır. Ancak, özgür iradenin yok edildiği bu süreç, insan doğası ve ahlaki seçimler üzerine derin sorular ortaya çıkarır.",
                ISBN = "9780393089134",
                Author = "Anthony Burgess",
                ListPrice = 9.99,
                Price = 8.99,
                Price50 = 8.49,
                Price100 = 7.99,

                //CategoryID = 3
            }

            );

        }


    }
}
