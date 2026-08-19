using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.ServiceContracts;

public interface IAuthService
{
    Task<Result> LoginAsync(LoginRequest request);
    Task<Result> RegisterAsync(RegisterRequest request);
}
