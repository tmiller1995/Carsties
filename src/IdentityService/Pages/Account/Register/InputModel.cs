using System.ComponentModel.DataAnnotations;

namespace IdentityService.Pages.Account.Register;

public class InputModel
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string Username { get; set; } = null!;
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}