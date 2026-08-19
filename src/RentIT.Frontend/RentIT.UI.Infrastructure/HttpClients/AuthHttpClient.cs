using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;

namespace RentIT.UI.Infrastructure.HttpClients;

public class AuthHttpClient : HttpClientBase, IAuthHttpClient
{
    public AuthHttpClient(HttpClient httpClient, IConfiguration config) 
        : base(httpClient, config) { }

    public async Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Register(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}
