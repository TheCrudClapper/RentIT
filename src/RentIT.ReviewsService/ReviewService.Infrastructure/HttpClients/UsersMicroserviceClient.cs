using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Users;
using System.Net.Http.Json;

namespace ReviewService.Infrastructure.HttpClients;

public class UsersMicroserviceClient : IUsersMicroserviceClient
{
    private readonly HttpClient _httpClient;
    public UsersMicroserviceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<UserDTO>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"/gateway/users/{userId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Failure<UserDTO>(Error.Create(ErrorType.Unexpected, ((int)response.StatusCode).ToString(), message));
        }

        UserDTO? userResponse = await response.Content.ReadFromJsonAsync<UserDTO>(cancellationToken);

        if (userResponse is null)
            return Result.Failure<UserDTO>(Error.Create(ErrorType.Unexpected, "500", "Invalid response from Users service"));

        return userResponse;
    }

    public async Task<Result<IEnumerable<UserDTO>>> GetUsersByUsersIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("/gateway/Users/query", userIds, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync(cancellationToken);
            return Result.Failure<IEnumerable<UserDTO>>(Error.Create(ErrorType.Unexpected, ((int)response.StatusCode).ToString(), message));
        }
        var userResponse = await response.Content.ReadFromJsonAsync<IEnumerable<UserDTO>>(cancellationToken);

        return Result.Success(userResponse ?? Enumerable.Empty<UserDTO>());
    }
}
