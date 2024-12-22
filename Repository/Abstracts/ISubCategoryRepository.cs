using ArchiveApp.Models;

namespace ArchiveApp.Repository.Abstracts;
public interface ISubCategoryRepository
{
  Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync();
  Task<SubCategory> GetByIdSubCategoryAsync(int? id);
  Task<SubCategory> AddSubCategoryAsync(SubCategory subCategory);
  Task UpdateSubCategoryAsync(int id, SubCategory subCategory);
  Task DeleteSubCategoryAsync(int id);
}