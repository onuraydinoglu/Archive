using ArchiveApp.Models;
using ArchiveApp.Repository;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArchiveApp.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductRepository _productRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    public ProductsController(IProductRepository productRepository, ISubCategoryRepository subCategoryRepository)
    {
      _productRepository = productRepository;
      _subCategoryRepository = subCategoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var product = await _productRepository.GetAllProductsAsync();
      return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.SubCategories = new SelectList(await _subCategoryRepository.GetAllSubCategoriesAsync(), "Id", "Name");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
      await _productRepository.AddProductAsync(product);
      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      ViewBag.SubCategories = new SelectList(await _subCategoryRepository.GetAllSubCategoriesAsync(), "Id", "Name");
      var prd = await _productRepository.GetByIdProductAsync(id);
      return View(prd);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
      await _productRepository.UpdateProductAsync(id, product);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      await _productRepository.DeleteProductAsync(id);
      return RedirectToAction("Index");
    }

  }
}