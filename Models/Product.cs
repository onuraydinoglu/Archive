namespace ArchiveApp.Models
{
  public sealed class Product : Entity<int, string, string>
  {
    public decimal Price { get; set; }
    public string? Store { get; set; }
    public string? Description { get; set; }
    public bool State { get; set; }
    public string? Url { get; set; }
    public int? SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }
  }
}