using System.ComponentModel.DataAnnotations;

namespace ArchiveApp.Models
{
  public abstract class Entity<TId, TName, TImage>
  {
    [Key]
    public TId Id { get; set; }
    public TName? Name { get; set; }
    public TImage? Image { get; set; }
  }
}
