namespace ArchiveApp.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; } = null!;
    }
}