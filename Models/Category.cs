namespace ArchiveApp.Models;

public sealed class Category : Entity<int, string, string>
{
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
