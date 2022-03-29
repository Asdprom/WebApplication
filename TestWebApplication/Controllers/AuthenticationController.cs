using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService = TestWebApplication.Services.AuthenticationService;

namespace TestWebApplication.Controllers;

public class AuthenticationController : Controller
{
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    // GET
    [HttpGet("/login")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login()
    {
        var context = HttpContext;
        var form = context.Request.Form;
        if (!form.ContainsKey("username") || !form.ContainsKey("password"))
            throw new UnauthorizedAccessException();

        var username = form["username"];
        var password = form["password"];

        if (!_authenticationService.Login(username, password))
            throw new InvalidOperationException();

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return RedirectToAction("Index", "Users");
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        var context = HttpContext;
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Authentication");
    }

    [Authorize]
    [HttpGet("/authenticated")]
    public string IsAuthenticated()
    {
        var userClaims = HttpContext.User.Claims;
        var name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(name))
        {
            HttpContext.Response.StatusCode = 401;
            return string.Empty;
        }

        return name;
    }

    [HttpGet]
    public IActionResult RegisterPage()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string userName, string password)
    {
        _authenticationService.Register(userName, password);
        return RedirectToAction("Index");
    }
}
