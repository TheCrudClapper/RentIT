using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.InfrastructureContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Core.ServiceContracts;

namespace RentIT.UI.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthHttpClient _httpClient;
    private readonly ITokenStore _tokenStore;
    public AuthService(IAuthHttpClient httpClient, ITokenStore tokenStore)
    {
        _httpClient = httpClient;
        _tokenStore = tokenStore;
    }

    public async Task<Result> LoginAsync(LoginRequest request)
    {
        Result<UserAuthResponse> result = await _httpClient.LoginAsync(request);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        _tokenStore.SaveAccessToken(result.Value.Token);
        return Result.Success();
    }

    public async Task<Result> RegisterAsync(RegisterRequest request) 
        => await _httpClient.Register(request);
}
