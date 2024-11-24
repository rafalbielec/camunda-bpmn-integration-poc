using System.ComponentModel.DataAnnotations;
using BpmnEngine.Application.Processors;

namespace BpmnEngine.Application.Models;

public class RoomBookingViewModel : BaseViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.RequiredLabel)]
    [Display(Name = PolishConstants.NumberOfPeopleLabel)]
    public int NumberOfPeople { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.RequiredLabel)]
    [Display(Name = PolishConstants.RoomLabel)]
    public string? Room { get; set; }
}