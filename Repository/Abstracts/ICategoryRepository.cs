using ArchiveApp.Models;

namespace ArchiveApp.Repository.Abstracts;

public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetAllCategoriesAsync();
  Task<Category> GetByIdCategoryAsync(int? id);
  Task<Category> AddCategoryAsync(Category category);
  Task UpdateCategoryAsync(int id, Category category, string newImagePath = null);
  Task DeleteCategoryAsync(int id);
}
