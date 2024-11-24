using System.ComponentModel.DataAnnotations;
using BpmnEngine.Application.Processors;

namespace BpmnEngine.Application.Models;

public class LoginViewModel : BaseViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.UserNameLabel)]
    [Display(Name = PolishConstants.UserNameLabel)]
    public string UserName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.PasswordLabel)]
    [Display(Name = PolishConstants.PasswordLabel)]
    public string Password { get; set; }
}