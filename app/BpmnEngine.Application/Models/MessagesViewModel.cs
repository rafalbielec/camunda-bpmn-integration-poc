using System.ComponentModel.DataAnnotations;
using BpmnEngine.Application.Processors;

namespace BpmnEngine.Application.Models;

public class MessagesViewModel : BaseViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.DestinationLabel)]
    [Display(Name = PolishConstants.BusinessKeyLabel)]
    public string BusinessKey { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = PolishConstants.MessageContentLabel)]
    [Display(Name = PolishConstants.MessageContentLabel)]
    public string MessageContent { get; set; }
}