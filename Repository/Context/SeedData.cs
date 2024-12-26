using ArchiveApp.Models;
using ArchiveApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository
{
  public static class SeedData
  {
    public static void TestData(IApplicationBuilder app)
    {
      var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<AppDbContext>();
      if (context != null)
      {
        if (context.Database.GetPendingMigrations().Any())
        {
          context.Database.Migrate();
        }
        if (!context.Categories.Any())
        {
          context.Categories.AddRange(
              new Category { Name = "Home" },
              new Category { Name = "Shopping" },
              new Category { Name = "Car" },
              new Category { Name = "Holiday" },
              new Category { Name = "Airbnb" }
          );
          context.SaveChanges();
        }

        if (!context.SubCategories.Any())
        {
          context.SubCategories.AddRange(
              new SubCategory { Name = "Beyaz Eşya", CategoryId = 1, Image = "ytk.jpeg" },
              new SubCategory { Name = "Oturma Grubu", CategoryId = 1, Image = "ytk.jpeg" },
              new SubCategory { Name = "Yatak Odası Takımı", CategoryId = 1, Image = "ytk.jpeg" },
              new SubCategory { Name = "Dresses", CategoryId = 2, Image = "ytk.jpeg" },
              new SubCategory { Name = "Make-Up", CategoryId = 2, Image = "ytk.jpeg" },
              new SubCategory { Name = "Gelinlik", CategoryId = 2, Image = "ytk.jpeg" },
              new SubCategory { Name = "Damatlık", CategoryId = 2, Image = "ytk.jpeg" },
              new SubCategory { Name = "Peugeot", CategoryId = 3, Image = "ytk.jpeg" },
              new SubCategory { Name = "Mercedes", CategoryId = 3, Image = "ytk.jpeg" }
          );
          context.SaveChanges();
        }

        if (!context.Products.Any())
        {
          context.Products.AddRange(
              new Product { Name = "Bulaşık Makinesi", SubCategoryId = 1, Price = 25000, Description = "Çok beğenerek aldık.", Store = "Arçelik", Url = "bulasik-makinesi", Image = "ytk.jpeg", State = true },

              new Product { Name = "Çamaşır Makinesi", SubCategoryId = 1, Price = 25000, Description = "Çok beğenerek aldık.", Store = "Arçelik", Url = "camasir-makinesi", Image = "ytk.jpeg", State = true },

              new Product { Name = "Buzdolabı", SubCategoryId = 1, Price = 25000, Description = "Çok beğenerek aldık.", Store = "Arçelik", Url = "buzdolabi", Image = "ytk.jpeg", State = true },

              new Product { Name = "Kurutma Makinesi", SubCategoryId = 1, Price = 25000, Description = "Çok beğenerek aldık.", Store = "Arçelik", Url = "kurutma-makinesi", Image = "ytk.jpeg" },

              new Product { Name = "L Koltuk", SubCategoryId = 2, Price = 25000, Description = "Ev küçük olursa L koltuk alınacak.", Store = "Doğtaş", Url = "l-koltuk", Image = "ytk.jpeg" },

              new Product { Name = "Meary", SubCategoryId = 3, Price = 25000, Description = "İstediğimiz takım.", Store = "Doğtaş", Url = "meary", Image = "ytk.jpeg" },

              new Product { Name = "Kazak", SubCategoryId = 4, Price = 250, Description = "Krem rengi", Store = "Koton", Url = "kazak", Image = "ytk.jpeg" },

              new Product { Name = "Ruj", SubCategoryId = 5, Price = 25, Description = "Kahve tonları", Store = "Mac", Url = "ruj", Image = "ytk.jpeg" }
          );
          context.SaveChanges();
        }
      }
    }
  }
}