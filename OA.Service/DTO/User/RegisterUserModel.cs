using System.ComponentModel.DataAnnotations;

namespace OA.Service.DTO.User;
public class RegisterUserModel
{
    [Display(Name = "Email")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "{0} is not valid")]
    public string Email { get; set; }

    [Display(Name = "Family")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    public string Family { get; set; }

    [Display(Name = "Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    public string Name { get; set; }

    [Display(Name = "Phone Number")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Password")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
    [MaxLength(length: 40, ErrorMessage = "maximum length {0} is 40 char")]
    [MinLength(length: 8, ErrorMessage = "minimum length {0} is 8 char")]
    public string Password { get; set; }

    [Display(Name = "Compare Password")]
    [Compare(nameof(Password), ErrorMessage = "{0} is not valid")]
    public string ComparePassword { get; set; }
}