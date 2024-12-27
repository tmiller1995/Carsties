using System.Security.Claims;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    [BindProperty] public InputModel Input { get; set; } = null!;
    [BindProperty] public bool RegisterSuccess { get; set; }

    public Index(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult OnGet(string? returnUrl)
    {
        Input = new InputModel()
        {
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Input.Button != "register") return Redirect("~/");

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
                return Redirect("~/"); // TODO: Change this to display the validation of what went wrong.

            await _userManager.AddClaimsAsync(user, [
                new Claim(JwtClaimTypes.Name, Input.FullName),
                new Claim(JwtClaimTypes.GivenName, Input.FirstName),
                new Claim(JwtClaimTypes.FamilyName, Input.LastName)
            ]);

            RegisterSuccess = true;
        }

        return Page();
    }
}