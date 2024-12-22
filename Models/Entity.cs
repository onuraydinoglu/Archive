namespace ArchiveApp.Models
{
  public abstract class Entity<TId, TName>
  {
    public TId Id { get; set; }
    public TName Name { get; set; }
  }
}
