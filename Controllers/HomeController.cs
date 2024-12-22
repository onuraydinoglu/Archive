using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArchiveApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryRepository.GetAllCategoriesAsync();
            return View(category);
        }
    }
}
