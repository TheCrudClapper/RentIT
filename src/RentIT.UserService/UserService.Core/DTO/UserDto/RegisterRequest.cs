using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserService.Core.Enums;

namespace UserService.Core.DTO.UserDto;
public class RegisterRequest
{
    [StringLength(50), Required]
    public string FirstName { get; set; } = null!;
    [StringLength(50), Required]
    public string LastName { get; set; } = null!;
    [StringLength(50), Required, EmailAddress]
    public string Email { get; set; } = null!;
    [StringLength(50), Required, PasswordPropertyText]
    public string Password { get; set; } = null!;
    public UserRoleOption UserRoleOption { get; set; } = UserRoleOption.User;
}