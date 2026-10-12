using RentIT.Contracts.DTO.UserMicroservice.User;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.HttpClientContracts;

public interface IAuthHttpClient
{
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request);
    Task<Result> Register(RegisterRequest request);
}
