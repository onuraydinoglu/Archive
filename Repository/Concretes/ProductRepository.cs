using System.Runtime.Intrinsics.Arm;
using ArchiveApp.Models;
using ArchiveApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository
{
  public class ProductRepository : IProductRepository
  {
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
      var product = await _context.Products.Include(x => x.SubCategory).ThenInclude(c => c.Category).ToListAsync();
      return product;
    }

    public async Task<Product> GetByIdProductAsync(int? id)
    {
      var product = await _context.Products.Include(x => x.SubCategory).ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);

      if (product is null)
      {
        throw new Exception($"Aradığınız id : {id} bulunamadı.");
      }

      return product;
    }

    public async Task<Product> GetByUrlProductAsync(string? url)
    {
      var product = await _context.Products.Include(x => x.SubCategory).ThenInclude(c => c.Category).FirstOrDefaultAsync(x => x.Url == url);
      if (product is null)
      {
        throw new Exception($"Aradığınız url : {url} bulunamadı.");
      }

      return product;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
      await _context.Products.AddAsync(product);
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task UpdateProductAsync(int id, Product product, string newImagePath = null)
    {
      var prd = await GetByIdProductAsync(id);

      // Update product properties
      prd.Name = product.Name;
      prd.Description = product.Description;
      prd.Star = product.Star;
      prd.Price = product.Price;
      prd.Store = product.Store;
      prd.SubCategoryId = product.SubCategoryId;
      prd.State = product.State;
      prd.Url = product.Url;
      prd.Link = product.Link;

      // Update the image if a new image path is provided
      if (!string.IsNullOrEmpty(newImagePath))
      {
        prd.Image = newImagePath;
      }

      _context.Update(prd);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
      var prd = await GetByIdProductAsync(id);
      _context.Remove(prd);
      await _context.SaveChangesAsync();
    }
  }
}