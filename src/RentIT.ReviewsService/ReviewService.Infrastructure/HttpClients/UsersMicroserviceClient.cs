using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.DTO;
using ReviewServices.Core.ResultTypes;
using System.Net.Http.Json;

namespace ReviewService.Infrastructure.HttpClients;

public class UsersMicroserviceClient : IUsersMicroserviceClient
{
    private readonly HttpClient _httpClient;
    public UsersMicroserviceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<UserResponse>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/gateway/users/{userId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Failure<UserResponse>(new Error((int)response.StatusCode, message));
        }

        UserResponse? userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();

        if(userResponse is null)
            return Result.Failure<UserResponse>(new Error(500, "Invalid response from Users service"));

        return userResponse;
    }
}
