namespace ArchiveApp.Models;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public bool State { get; set; }

    public int? CategoryId { get; set; }
    public Category Category { get; set; }
}
