using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OA.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [MaxLength(length: 40)]
    public string Name { get; set; } = "";

    [MaxLength(length: 40)]
    public string Family { get; set; } = "";

    public virtual ICollection<TodoEntity> Todos { get; set; }
}