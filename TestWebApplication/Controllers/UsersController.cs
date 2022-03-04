using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Models;
using TestWebApplication.Services;

namespace TestWebApplication.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly UsersService _usersService;

    public UsersController(ILogger<UsersController> logger, UsersService usersService)
    {
        _logger = logger;
        _usersService = usersService;
    }

    public IActionResult Index()
    {
        var users = _usersService.GetUsers();
        ViewBag.Users = users;
        return View();
    }

    [HttpPost]
    public IActionResult Users(User user)
    {
        _usersService.AddUser(user);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/edit/{id}")]
    public new IActionResult User(Guid id)
    {
        var user = _usersService.GetUsers().FirstOrDefault(x => x.Id == id);
        return View(user);
    }

    [HttpPost("/edit")]
    public IActionResult Edit(User user)
    {
        _usersService.UpdateUser(user);
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete("/delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        _usersService.DeleteUser(id);
        return Ok();
    }
}
