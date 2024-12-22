namespace ArchiveApp.Models;

public sealed class Category : Entity<int, string>
{
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
