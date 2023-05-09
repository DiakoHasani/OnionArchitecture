using System.ComponentModel.DataAnnotations;

namespace OA.Service.DTO.Todo;
public class PostTodoModel
{
    [Display(Name = "Title")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    [MaxLength(length: 200, ErrorMessage = "maximum {0} is 200 chars")]
    public string Title { get; set; }

    [Display(Name = "Title")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    public string Description { get; set; }

    [Display(Name = "Date")]
    public DateTime Date { get; set; }

}