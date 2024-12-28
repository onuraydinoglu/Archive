using ArchiveApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveApp.ViewComponents
{
    public class ProductsList : ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public ProductsList(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IViewComponentResult> Invoke()
        {
            var product = await _productRepository.GetAllProductsAsync();
            return View(product);
        }
    }
}