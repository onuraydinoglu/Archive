using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArchiveApp.Controllers;

public class SubCategoryController : Controller
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SubCategoryController(ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await _subCategoryRepository.GetAllSubCategoriesAsync();
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubCategory subCategory)
    {
        await _subCategoryRepository.AddSubCategoryAsync(subCategory);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var subCategory = await _subCategoryRepository.GetByIdSubCategoryAsync(id);
        ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
        return View(subCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, SubCategory subCategory)
    {
        await _subCategoryRepository.UpdateSubCategoryAsync(id, subCategory);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _subCategoryRepository.DeleteSubCategoryAsync(id);
        return RedirectToAction("Index");
    }
}