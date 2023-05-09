using System.ComponentModel.DataAnnotations;

namespace OA.Service.DTO.User;
public class LoginBodyModel
{
    [Display(Name ="UserName")]
    [Required(AllowEmptyStrings =false,ErrorMessage ="{0} is required")]
    public string UserName { get; set; }

    [Display(Name ="Password")]
    [Required(AllowEmptyStrings =false,ErrorMessage ="{0} is required")]
    public string Password { get; set; }
}