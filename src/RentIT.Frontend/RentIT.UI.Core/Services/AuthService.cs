using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Core.ServiceContracts;

namespace RentIT.UI.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthHttpClient _httpClient;
    public AuthService(IAuthHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result> LoginAsync(LoginRequest request)
    {
        Result<UserAuthResponse> result = await _httpClient.LoginAsync(request);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        //add token do store

        return Result.Success();
    }

    public async Task<Result> RegisterAsync(RegisterRequest request) 
        => await _httpClient.Register(request);
}
