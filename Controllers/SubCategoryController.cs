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
                subCategory.Image = uniqueFileName;
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
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Generate a unique file name
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

                // Pass the new image path to the repository
                string newImagePath = uniqueFileName;

                // Delegate update to repository
                await _subCategoryRepository.UpdateSubCategoryAsync(id, subCategory, newImagePath);
            }
            else
            {
                // Update without changing the image
                await _subCategoryRepository.UpdateSubCategoryAsync(id, subCategory);
            }

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
