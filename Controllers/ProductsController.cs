using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
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
        // wwwroot/image klasörüne yükleme
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");

        // Klasör yoksa oluştur
        if (!Directory.Exists(uploadPath))
        {
          Directory.CreateDirectory(uploadPath);
        }

        // Benzersiz dosya adı oluştur
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

        // Tam dosya yolu
        string filePath = Path.Combine(uploadPath, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          await ImageFile.CopyToAsync(fileStream);
        }

        // Dosya yolunu modelde sakla
        product.Image = "/image/" + uniqueFileName;
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
      var existingProduct = await _productRepository.GetByIdProductAsync(id);
      if (existingProduct == null)
      {
        return NotFound();
      }

      // Ürün bilgilerini güncelle
      existingProduct.Name = product.Name;
      existingProduct.SubCategoryId = product.SubCategoryId;

      // Yeni bir resim yüklendiyse işle
      if (ImageFile != null && ImageFile.Length > 0)
      {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");

        // Benzersiz dosya adı oluştur
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

        // Tam dosya yolu
        string filePath = Path.Combine(uploadPath, uniqueFileName);

        // Eski resmi sil
        if (!string.IsNullOrEmpty(existingProduct.Image))
        {
          string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingProduct.Image.TrimStart('/'));
          if (System.IO.File.Exists(oldFilePath))
          {
            System.IO.File.Delete(oldFilePath);
          }
        }

        // Yeni resmi kaydet
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
          await ImageFile.CopyToAsync(fileStream);
        }

        // Yeni dosya yolunu güncelle
        existingProduct.Image = "/image/" + uniqueFileName;
      }

      await _productRepository.UpdateProductAsync(id, existingProduct);
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
    public async Task<IActionResult> Details(string url)
    {
      var product = await _productRepository.GetByUrlProductAsync(url);
      return View(product);
    }
  }
}
