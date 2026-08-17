using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserService.Core.Enums;

namespace UserService.Core.DTO.User;

public record UserAddRequest(
    [StringLength(50), Required] string FirstName,
    [StringLength(50), Required] string LastName,
    [StringLength(50), Required, EmailAddress] string Email,
    [StringLength(50), Required, PasswordPropertyText] string Password,
    UserRoleOption UserRoleType);
