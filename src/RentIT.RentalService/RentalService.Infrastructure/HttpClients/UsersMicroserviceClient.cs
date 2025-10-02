using Polly.CircuitBreaker;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.UserDto;
using RentalService.Core.ResultTypes;
using System.Net.Http.Json;

namespace RentalService.Infrastructure.HttpClients
{
    public class UsersMicroserviceClient : IUsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public UsersMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<UserDTO?>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/gateway/users/{userId}", cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync(cancellationToken);
                    return Result.Failure<UserDTO?>(new Error((int)response.StatusCode, message));
                }

                UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>(cancellationToken);

                if (user == null)
                    return Result.Failure<UserDTO?>(new Error(500, "Invalid response from Users service"));

                return Result.Success<UserDTO?>(user);
            }
            catch (BrokenCircuitException)
            {
                return Result.Failure<UserDTO?>(new Error(503, "Service unavaliable, try again later"));
            }
           
        }
    }
}
