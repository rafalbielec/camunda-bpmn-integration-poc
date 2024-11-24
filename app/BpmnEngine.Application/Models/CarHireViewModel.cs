using System.ComponentModel.DataAnnotations;
using BpmnEngine.Application.Processors;

namespace BpmnEngine.Application.Models;

public class CarHireViewModel : BaseViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.RequiredLabel)]
    [Display(Name = PolishConstants.PhoneNumberLabel)]
    public string? PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.RequiredLabel)]
    [Display(Name = PolishConstants.DestinationLabel)]
    public string? Destination { get; set; }
}