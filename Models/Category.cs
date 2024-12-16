namespace ArchiveApp.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
