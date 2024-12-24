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
            var products = await _productRepository.GetAllProductsAsync(); // Eğer ürünleri de gösterecekseniz.

            // HomeViewModel'i oluşturun ve kategorileri, ürünleri ekleyin.
            var viewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel); // View'e HomeViewModel gönderiliyor.
        }

    }
}
