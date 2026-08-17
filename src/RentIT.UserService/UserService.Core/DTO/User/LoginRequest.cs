using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserService.Core.DTO.UserDto;

public class LoginRequest
{
    [StringLength(50), Required, EmailAddress]
    public string Email { get; set; } = null!;
    [StringLength(50), Required, PasswordPropertyText]
    public string Password { get; set; } = null!;
}