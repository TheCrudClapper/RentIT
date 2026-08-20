using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Auth;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RentIT.UI.Infrastructure.HttpClients;

public class AuthHttpClient : HttpClientBase, IAuthHttpClient
{
    public AuthHttpClient(HttpClient httpClient, IConfiguration config) 
        : base(httpClient, config) { }

    public async Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            try
            {
                var details = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                if (details is null)
                    return Result.Failure<UserAuthResponse>(Error.Create("Payload is null", "Response is null"));

                return Result.Failure<UserAuthResponse>(Error.Create(details.Title, details.Detail));
            }
            catch
            {
                return Result.Failure<UserAuthResponse>(Error.Create("Unexpected", "Something unexpected happend."));
            }
        }

        var content = await response.Content.ReadFromJsonAsync<UserAuthResponse>();
        if (content is null)
            return Result.Failure<UserAuthResponse>(Error.Create("Payload is null", "Response is null"));

        return content;
    }

    public async Task<Result> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", request);

        if (!response.IsSuccessStatusCode)
        {
            try
            {
                var details = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                if (details is null)
                    return Result.Failure(Error.Create("Payload is null", "Response is null"));

                return Result.Failure(Error.Create(details.Title, details.Detail));
            }
            catch
            {
                return Result.Failure(Error.Create("Unexpected", "Something unexpected happend."));
            }
        }

        return Result.Success();
    }
}
