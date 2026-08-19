using RentIT.BlazorFrontend.Models.Auth;
using RentIT.UI.Core.DTO.Auth;

namespace RentIT.BlazorFrontend.Mappings;
public static class AuthMappings
{
    public static LoginRequest ToDto(this LoginModel model)
        => new() { Email = model.Email, Password = model.Password };

    public static RegisterRequest ToDto(this RegisterModel model)
        => new() { FirstName = model.FirstName, Email = model.Email, Password = model.Password, LastName = model.LastName };
}
