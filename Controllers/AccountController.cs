using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SchoolMaterials.Models;

namespace SchoolMaterials.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender; // НОВО: Добавяме нашия изпращач

    // НОВО: Добавяме IEmailSender в конструктора
    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailSender emailSender) 
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(
        RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(
            user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Teacher");

            // --- НОВО: Генериране на линк за потвърждение ---
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            
            var callbackUrl = Url.Action(
                action: "ConfirmEmail",
                controller: "Account",
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);

            // "Изпращаме" имейла (ще се принтира в терминала)
            await _emailSender.SendEmailAsync(model.Email, "Потвърдете своя акаунт",
                $"Моля, потвърдете акаунта си, като <a href='{callbackUrl}'>кликнете тук</a>.");

            // Пренасочваме към Login страницата, вместо да го логваме директно
            return RedirectToAction("Login", "Account");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // НОВО: Методът, който се извиква, когато цъкнеш на линка
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
            return RedirectToAction("Index", "Home");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound($"Грешка при зареждане на потребител с ID '{userId}'.");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        
        if (result.Succeeded)
        {
            // Успешно потвърден имейл! Може да влезе.
            return RedirectToAction("Login", "Account");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(
            string.Empty,
            "Невалиден имейл или парола (или имейлът ви не е потвърден).");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}