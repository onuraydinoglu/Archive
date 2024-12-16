namespace ArchiveApp.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool State { get; set; }

    public int? CategoryId { get; set; }
    public Category Category { get; set; }
}
