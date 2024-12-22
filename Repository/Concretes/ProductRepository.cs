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
      var product = await _context.Products.Include(x => x.SubCategory).ToListAsync();
      return product;
    }

    public async Task<Product> GetByIdProductAsync(int? id)
    {
      var product = await _context.Products.FindAsync(id);

      if (product is null)
      {
        throw new Exception($"Aradığınız is : {id} bulunamadı.");
      }

      return product;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
      await _context.Products.AddAsync(product);
      await _context.SaveChangesAsync();
      return product;
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
      var prd = await GetByIdProductAsync(id);
      prd.Name = product.Name;
      prd.Description = product.Description;
      prd.Price = product.Price;
      prd.Store = product.Store;
      prd.SubCategoryId = product.SubCategoryId;
      prd.State = product.State;
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