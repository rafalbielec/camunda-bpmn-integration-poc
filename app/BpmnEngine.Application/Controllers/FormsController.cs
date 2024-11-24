using System.Diagnostics;
using BpmnEngine.Application.Models;
using BpmnEngine.Application.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("forms")]
[Authorize]
public class FormsController : Controller
{
    private readonly IViewModelProvider _provider;
    private readonly IViewModelProcessor _processor;

    public FormsController(IViewModelProvider provider, IViewModelProcessor processor)
    {
        _provider = provider;
        _processor = processor;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var model = await _provider.GetFormsAsync();

        return View(model);
    }
    
    [HttpGet("messages")]
    [AllowAnonymous]
    public IActionResult Messages()
    {
        var model = new MessagesViewModel();

        return View(model);
    }

    [HttpPost("messages")]
    [AllowAnonymous]
    public async Task<IActionResult> Messages(MessagesViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var viewModel = await _processor.ProcessMessagesViewModelAsync(model);

        return View("MessageInfo", viewModel);
    }

    [HttpGet("car_hire")]
    public async Task<IActionResult> CarHire()
    {
        var model = await _provider.GetCarHireAsync();

        return View(model);
    }

    [HttpPost("car_hire")]
    public async Task<IActionResult> CarHire(CarHireViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (User.Identity is {IsAuthenticated: true})
        {
            var viewModel = await _processor.ProcessViewModelAsync(model, User.Identity);
        
            return View("ProcessInfo", viewModel);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("room_booking")]
    public async Task<IActionResult> RoomBooking()
    {
        var model = await _provider.GetRoomBookingAsync();

        return View(model);
    }
    
    [HttpPost("room_booking")]
    public async Task<IActionResult> RoomBooking(RoomBookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (User.Identity is { IsAuthenticated: true })
        {
            var viewModel = await _processor.ProcessViewModelAsync(model, User.Identity);

            return View("ProcessInfo", viewModel);
        }

        return RedirectToAction("Index", "Home");
    }
}