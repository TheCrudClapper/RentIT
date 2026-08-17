using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserService.Core.Enums;

namespace UserService.Core.DTO.UserDto;
public class RegisterRequest
{
    [StringLength(50), Required]
    public string FirstName { get; init; } = null!;
    [StringLength(50), Required]
    public string LastName { get; init; } = null!;
    [StringLength(50), Required, EmailAddress]
    public string Email { get; init; } = null!;
    [StringLength(50), Required, PasswordPropertyText]
    public string Password { get; init; } = null!;
    public UserRoleOption UserRoleType { get; set; } = UserRoleOption.User;
}