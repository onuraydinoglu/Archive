namespace ArchiveApp.Models
{
  public class HomeViewModel
  {
    public IEnumerable<Category> Categories { get; set; } = null!;
    public IEnumerable<Product> Products { get; set; } = null!;
  }
}