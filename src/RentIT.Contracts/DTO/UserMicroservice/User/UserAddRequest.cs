using RentIT.Contracts.DTO.UserMicroservice.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentIT.Contracts.DTO.UserMicroservice.User;

public record UserAddRequest(
    [StringLength(50), Required] string FirstName,
    [StringLength(50), Required] string LastName,
    [StringLength(50), Required, EmailAddress] string Email,
    [StringLength(50), Required, PasswordPropertyText] string Password,
    UserRoles UserRoleType);


