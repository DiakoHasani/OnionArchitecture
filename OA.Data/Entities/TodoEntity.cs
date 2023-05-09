using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Data.Entities;

[Table("Todo")]
public class TodoEntity : BaseEntity
{
    [Required]
    [MaxLength(length: 200)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public bool IsDelete { get; set; } = false;

    public DateTime Date { get; set; }

    public bool Done { get; set; } = false;

    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}