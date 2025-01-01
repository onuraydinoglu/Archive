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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var product = await _productRepository.GetAllProductsAsync();

            return View(product);
        }
    }
}