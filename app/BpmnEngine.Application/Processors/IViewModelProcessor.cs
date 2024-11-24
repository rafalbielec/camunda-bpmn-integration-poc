using System.Security.Principal;
using BpmnEngine.Application.Models;

namespace BpmnEngine.Application.Processors;

public interface IViewModelProcessor
{
    Task<ProcessInfoViewModel> ProcessViewModelAsync<T>(T model, IIdentity userIdentity) where T : BaseViewModel;
    Task<MessageInfoViewModel> ProcessMessagesViewModelAsync(MessagesViewModel model);
}