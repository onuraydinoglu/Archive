using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using ArchiveApp.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository.Concretes;
public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly AppDbContext _context;

    public SubCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync()
    {
        var subCategorys = await _context.SubCategories.Include(x => x.Category).ToListAsync();
        return subCategorys;
    }

    public async Task<SubCategory> GetByIdSubCategoryAsync(int? id)
    {
        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory is null)
        {
            throw new Exception($"Aradığınız id: {id} bulunamadı.");
        }
        return subCategory;
    }

    public async Task<SubCategory> AddSubCategoryAsync(SubCategory subCategory)
    {
        await _context.SubCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();
        return subCategory;
    }

    public async Task UpdateSubCategoryAsync(int id, SubCategory subCategory, string newImagePath = null)
    {
        var existingSubCategory = await GetByIdSubCategoryAsync(id);

        // Update sub-category properties
        existingSubCategory.Name = subCategory.Name;
        existingSubCategory.CategoryId = subCategory.CategoryId;

        // Update the image if a new image path is provided
        if (!string.IsNullOrEmpty(newImagePath))
        {
            existingSubCategory.Image = newImagePath;
        }

        _context.SubCategories.Update(existingSubCategory);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteSubCategoryAsync(int id)
    {
        var subCategory = await GetByIdSubCategoryAsync(id);
        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();
    }
}