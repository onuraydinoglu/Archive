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
              new Category { Name = "Home", Image = "ytk.jpeg" },
              new Category { Name = "Shopping", Image = "ytk.jpeg" },
              new Category { Name = "Car", Image = "ytk.jpeg" },
              new Category { Name = "Holiday", Image = "ytk.jpeg" },
              new Category { Name = "Airbnb", Image = "ytk.jpeg" }
          );
          context.SaveChanges();
        }

        if (!context.SubCategories.Any())
        {
          context.SubCategories.AddRange(
              new SubCategory
              {
                Name = "Beyaz Eşya",
                CategoryId = 1,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Oturma Grubu",
                CategoryId = 1,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Yatak Odası Takımı",
                CategoryId = 1,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Dresses",
                CategoryId = 2,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Make-Up",
                CategoryId = 2,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Gelinlik",
                CategoryId = 2,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Damatlık",
                CategoryId = 2,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Peugeot",
                CategoryId = 3,
                Image = "ytk.jpeg"
              },
              new SubCategory
              {
                Name = "Mercedes",
                CategoryId = 3,
                Image = "ytk.jpeg"
              }
          );
          context.SaveChanges();
        }

        if (!context.Products.Any())
        {
          context.Products.AddRange(
              new Product
              {
                Name = "Buzdolabı",
                SubCategoryId = 1,
                Price = 25000,
                Description = "Çok beğenerek aldık.",
                Star = 5,
                Store = "Arçelik",
                Url = "buzdolabi",
                Image = "Buzdolabi.webp",
                State = true,
                Link = "https://www.arcelik.com.tr/alttan-donduruculu-buzdolabi/270490-ei-buzdolabi"
              },

              new Product
              {
                Name = "Bulaşık Makinesi",
                SubCategoryId = 1,
                Price = 25000,
                Description = "Çok beğenerek aldık.",
                Star = 5,
                Store = "Arçelik",
                Url = "bulasik-makinesi",
                Image = "Bulasik.webp",
                State = true,
                Link = "https://www.arcelik.com.tr/bulasik-makinesi/6444-ok-i-bulasik-makinesi"
              },

              new Product
              {
                Name = "Çamaşır Makinesi",
                SubCategoryId = 1,
                Price = 25000,
                Description = "Çok beğenerek aldık.",
                Star = 5,
                Store = "Arçelik",
                Url = "camasir-makinesi",
                Image = "camasir.webp",
                State = true,
                Link = "https://www.arcelik.com.tr/camasir-makinesi/9120-dmxs-camasir-makinesi"
              },

              new Product
              {
                Name = "Kurutma Makinesi",
                SubCategoryId = 1,
                Price = 25000,
                Description = "Çok beğenerek aldık.",
                Star = 5,
                Store = "Arçelik",
                Url = "kurutma-makinesi",
                Image = "Kurutma.webp",
                State = true,
                Link = "https://www.arcelik.com.tr/kurutma-makinesi/900-kmx-s-kurutma-makinesi"
              },

              new Product
              {
                Name = "L Koltuk",
                SubCategoryId = 2,
                Price = 25000,
                Description = "Ev küçük olursa L koltuk alınacak.",
                Star = 3,
                Store = "Doğtaş",
                Url = "l-koltuk",
                Image = "LKoltuk.jpeg",
                State = true,
                Link = "https://www.dogtas.com/mila-kose-takimi"
              },

              new Product
              {
                Name = "Meary",
                SubCategoryId = 3,
                Price = 25000,
                Description = "İstediğimiz takım.",
                Star = 4,
                Store = "Doğtaş",
                Url = "meary",
                Image = "Yatak.jpeg",
                State = true,
                Link = "https://www.dogtas.com/bend-yatak-odasi"
              },

              new Product
              {
                Name = "Kazak",
                SubCategoryId = 4,
                Price = 250,
                Description = "Krem rengi",
                Star = 3,
                Store = "Koton",
                Url = "kazak",
                Image = "Kazak.Webp",
                State = true,
                Link = "https://koton.com/uzun-kollu-dokulu-slim-fit-dik-yaka-triko-kazak-3932813/?_gl=1*vd0rth*_up*MQ..*_ga*NDUyMTQwODYyLjE3MzUzMDIwNjc.*_ga_5JXKCHWYLW*MTczNTMwMjA2NS4xLjEuMTczNTMwMjA2NS4wLjAuMA.."
              },

              new Product
              {
                Name = "Ruj",
                SubCategoryId = 5,
                Price = 25,
                Description = "Kahve tonları",
                Star = 1,
                Store = "Mac",
                Url = "ruj",
                Image = "Ruj.Jpg",
                State = true,
                Link = "https://www.maccosmetics.com.tr/product/18579/123863/urunler/collections/all-products/macximal-silky-matte-lipstick"
              }
          );
          context.SaveChanges();
        }
      }
    }
  }
}