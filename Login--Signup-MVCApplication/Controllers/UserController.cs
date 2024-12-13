using Login__Signup_MVCApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Login__Signup_MVCApplication.Controllers;

public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
    {
        var model = new RegisterViewModel();
        return View(model);
    }

    public IActionResult Register()
    {
        var model = new RegisterViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Email };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var loginModel = new RegisterViewModel();
            loginModel.IsActionSuccess = true;
            loginModel.ActionMessage = @"You have succesfuly Register." +
                "                       You can now use your credentials to login!!";
            return RedirectToAction("Login");
        }

        model.IsActionSuccess = false;
        model.ActionMessage = @"- Password Must Contain one or more Upper Case \n
                                        - Password Must Contain one or more Lower Case \n
                                        - Password Must Contain one or more numbers \n
                                        - Password Must Contain one or more Special Characters";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(RegisterViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            model.IsActionSuccess = true;
            model.ActionMessage = "Great you are now logged in!!";

            return RedirectToAction("Index", "Home", model);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
