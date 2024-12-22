using ArchiveApp.Models;

namespace ArchiveApp.Repository
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetByIdProductAsync(int? id);
    Task<Product> AddProductAsync(Product product);
    Task UpdateProductAsync(int id, Product product);
    Task DeleteProductAsync(int id);
  }
}