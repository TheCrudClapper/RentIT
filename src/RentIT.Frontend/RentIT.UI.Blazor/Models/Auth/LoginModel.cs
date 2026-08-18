using System.ComponentModel.DataAnnotations;

namespace RentIT.BlazorFrontend.Models.Auth;
public class LoginModel
{
    [Required(ErrorMessage = "Email jest wymagany.")]
    [EmailAddress(ErrorMessage = "Podana fraza musi być prawidłowyn emailem.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane.")]
    [MinLength(6, ErrorMessage = "Hasło musi zawierać co najmniej 6 znaków.")]
    public string Password { get; set; } = string.Empty;
}
