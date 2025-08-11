using UserService.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace RentIT.Core.DTO.UserDto
{
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
}
