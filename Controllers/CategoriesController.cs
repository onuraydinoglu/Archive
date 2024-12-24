using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArchiveApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(Category category, IFormFile ImageFile)
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
                category.Image = "/image/" + uniqueFileName;
            }

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
        public async Task<IActionResult> Edit(int id, Category category, IFormFile ImageFile)
        {
            // Veritabanındaki mevcut kategori bilgilerini al
            var existingCategory = await _categoryRepository.GetByIdCategoryAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            // Kategori bilgilerini güncelle
            existingCategory.Name = category.Name;

            // Yeni bir resim yüklendiyse işle
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // wwwroot/image klasörüne yükleme
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");

                // Benzersiz dosya adı oluştur
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

                // Tam dosya yolu
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                // Eski resmi sil
                if (!string.IsNullOrEmpty(existingCategory.Image))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingCategory.Image.TrimStart('/'));
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
                existingCategory.Image = "/image/" + uniqueFileName;
            }

            // Veritabanındaki güncellemeyi kaydet
            await _categoryRepository.UpdateCategoryAsync(id, existingCategory);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
