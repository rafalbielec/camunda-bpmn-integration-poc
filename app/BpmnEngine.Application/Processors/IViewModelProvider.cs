using BpmnEngine.Application.Models;

namespace BpmnEngine.Application.Processors;

public interface IViewModelProvider
{
    Task<CarHireViewModel> GetCarHireAsync();
    Task<RoomBookingViewModel> GetRoomBookingAsync();
    Task<FormsViewModel> GetFormsAsync();
}