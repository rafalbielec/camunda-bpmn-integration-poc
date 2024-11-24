using System.Diagnostics;
using BpmnEngine.Application.Models;
using BpmnEngine.Application.Processors;
using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IUsersRepository _repository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        SignInManager<UserEntity> signInManager,
        IUsersRepository repository,
        ILogger<HomeController> logger)
    {
        _signInManager = signInManager;
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        _logger.LogInformation("Website visit");

        return View(new LoginViewModel());
    }
    
    [HttpPost("/")]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        _logger.LogInformation("Website visit");

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, 
                model.Password, 
                false, 
                false);

            if (result.Succeeded)
            {
                var user = await _repository.SelectByUserName(model.UserName);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false);
                }

                return RedirectToAction("Index", "Forms");
            }

            ModelState.AddModelError("password", PolishConstants.WrongCredentials);
        }

        return View(model);
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet("/error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}