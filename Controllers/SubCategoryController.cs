using ArchiveApp.Models;
using ArchiveApp.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArchiveApp.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubCategoryController(
            ISubCategoryRepository subCategoryRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subCategories = await _subCategoryRepository.GetAllSubCategoriesAsync();
            return View(subCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategory subCategory, IFormFile ImageFile)
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
                subCategory.Image = "/image/" + uniqueFileName;
            }

            await _subCategoryRepository.AddSubCategoryAsync(subCategory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subCategory = await _subCategoryRepository.GetByIdSubCategoryAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SubCategory subCategory, IFormFile ImageFile)
        {
            var existingSubCategory = await _subCategoryRepository.GetByIdSubCategoryAsync(id);
            if (existingSubCategory == null)
            {
                return NotFound();
            }

            // SubCategory bilgilerini güncelle
            existingSubCategory.Name = subCategory.Name;
            existingSubCategory.CategoryId = subCategory.CategoryId;

            // Yeni bir resim yüklendiyse işle
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");

                // Benzersiz dosya adı oluştur
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

                // Tam dosya yolu
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                // Eski resmi sil
                if (!string.IsNullOrEmpty(existingSubCategory.Image))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingSubCategory.Image.TrimStart('/'));
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
                existingSubCategory.Image = "/image/" + uniqueFileName;
            }

            await _subCategoryRepository.UpdateSubCategoryAsync(id, existingSubCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _subCategoryRepository.DeleteSubCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
