using BpmnEngine.Application.Models;
using BpmnEngine.Services.Extensions;
using BpmnEngine.Storage;
using BpmnEngine.Storage.Abstractions;

namespace BpmnEngine.Application.Processors;

public class ViewModelProvider : IViewModelProvider
{
    private readonly IFormsRepository _repository;

    public ViewModelProvider(IFormsRepository repository)
    {
        _repository = repository;
    }

    public async Task<CarHireViewModel> GetCarHireAsync()
    {
        var form = await _repository.SelectFormByProcessNameAsync(StorageConstants.ProcessName.CarHire);

        var model = new CarHireViewModel
        {
            PageTitle = form.Name
        };

        return model;
    }

    public async Task<RoomBookingViewModel> GetRoomBookingAsync()
    {
        var form = await _repository.SelectFormByProcessNameAsync(StorageConstants.ProcessName.RoomBooking);

        var model = new RoomBookingViewModel
        {
            PageTitle = form.Name
        };

        return model;
    }

    public async Task<FormsViewModel> GetFormsAsync()
    {
        var forms = await _repository.SelectFormsAsync();

        var model = new FormsViewModel();

        model.Forms.AddRange(forms.Select(a =>
            new FormLinkViewModel(a.Name, a.ProcessName.ToString()
                .ToSnakeCase())));

        return model;
    }
}