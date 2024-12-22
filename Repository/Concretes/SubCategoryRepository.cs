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

    public async Task UpdateSubCategoryAsync(int id, SubCategory subCategory)
    {
        var mvi = await GetByIdSubCategoryAsync(id);
        mvi.Name = subCategory.Name;
        mvi.CategoryId = subCategory.CategoryId;
        _context.SubCategories.Update(mvi);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubCategoryAsync(int id)
    {
        var subCategory = await GetByIdSubCategoryAsync(id);
        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();
    }
}