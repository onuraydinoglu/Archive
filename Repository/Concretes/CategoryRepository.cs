using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using ArchiveApp.Repository.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository.Concretes;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> GetByIdCategoryAsync(int? id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
        {
            throw new Exception($"Aradınız id: {id} bulunamadı.");
        }
        return category;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        var ctg = await GetByIdCategoryAsync(id);
        ctg.Name = category.Name;
        _context.Categories.Update(ctg);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await GetByIdCategoryAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}