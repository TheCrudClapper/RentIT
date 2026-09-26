using RentIT.UI.Contracts.DTO.Auth;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.ServiceContracts;

public interface IAuthService
{
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request);
    Task<Result> RegisterAsync(RegisterRequest request);
}
