namespace ArchiveApp.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
