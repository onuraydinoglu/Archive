using ArchiveApp.Models;
using ArchiveApp.Repository;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArchiveApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public HomeController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return View(categories);
        }

    }
}
