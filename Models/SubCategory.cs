namespace ArchiveApp.Models;

public sealed class SubCategory : Entity<int, string, string>
{
    public int? CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
