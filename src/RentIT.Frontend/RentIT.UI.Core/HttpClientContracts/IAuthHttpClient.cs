using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.HttpClientContracts;

public interface IAuthHttpClient
{
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request);
    Task<Result>Register(RegisterRequest request);
}
