using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var category = await _categoryRepository.GetAllCategoriesAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdCategoryAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(id, category);
            return RedirectToAction("Index", "Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _categoryRepository.GetByIdCategoryAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
