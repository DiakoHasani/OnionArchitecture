using System.ComponentModel.DataAnnotations;

namespace OA.Data.Entities;
public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
}