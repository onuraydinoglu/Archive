using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ArchiveApp.Repository;

namespace ArchiveApp.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductRepository _productRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(
        IProductRepository productRepository,
        ISubCategoryRepository subCategoryRepository,
        IWebHostEnvironment webHostEnvironment)
    {
      _productRepository = productRepository;
      _subCategoryRepository = subCategoryRepository;
      _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var products = await _productRepository.GetAllProductsAsync();
      return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.SubCategories = new SelectList(await _subCategoryRepository.GetAllSubCategoriesAsync(), "Id", "Name");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
    {
      if (ImageFile != null && ImageFile.Length > 0)
      {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");

        if (!Directory.Exists(uploadPath))
        {
          Directory.CreateDirectory(uploadPath);
        }

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

        string filePath = Path.Combine(uploadPath, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          await ImageFile.CopyToAsync(fileStream);
        }

        product.Image = uniqueFileName;
      }

      await _productRepository.AddProductAsync(product);
      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      ViewBag.SubCategories = new SelectList(await _subCategoryRepository.GetAllSubCategoriesAsync(), "Id", "Name");
      var product = await _productRepository.GetByIdProductAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile ImageFile)
    {
      if (ImageFile != null && ImageFile.Length > 0)
      {
        // Generate a new unique file name
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");
        if (!Directory.Exists(uploadPath))
        {
          Directory.CreateDirectory(uploadPath);
        }
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
        string filePath = Path.Combine(uploadPath, uniqueFileName);

        // Save the new image
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          await ImageFile.CopyToAsync(fileStream);
        }

        // Pass the new image path to the repository for updating
        string newImagePath = uniqueFileName;

        // Delegate update to repository
        await _productRepository.UpdateProductAsync(id, product, newImagePath);
      }
      else
      {
        // Update without changing the image
        await _productRepository.UpdateProductAsync(id, product);
      }

      return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
      var product = await _productRepository.GetByIdProductAsync(id);

      if (product != null && !string.IsNullOrEmpty(product.Image))
      {
        // Resmi sil
        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, product.Image.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
          System.IO.File.Delete(filePath);
        }
      }

      await _productRepository.DeleteProductAsync(id);
      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(string? url)
    {
      var product = await _productRepository.GetByUrlProductAsync(url);
      var products = await _productRepository.GetAllProductsAsync();

      var modelView = new ProductViewModel
      {
        Product = product,
        Products = products
      };

      return View(modelView);
    }
  }
}

