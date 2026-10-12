using RentIT.Contracts.DTO.UserMicroservice.User;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.ServiceContracts;

public interface IAuthService
{
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request);
    Task<Result> RegisterAsync(RegisterRequest request);
}
