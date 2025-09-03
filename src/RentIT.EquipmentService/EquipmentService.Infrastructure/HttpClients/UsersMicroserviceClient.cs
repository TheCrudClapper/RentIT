using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.DTO.UserDto;
using EquipmentService.Core.ResultTypes;
using System.Net.Http.Json;

namespace EquipmentService.Infrastructure.HttpClients
{
    public class UsersMicroserviceClient : IUsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public UsersMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<UserDTO?>> GetUserByUserId(Guid userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Result.Failure<UserDTO?>(new Error((int)response.StatusCode, message));
            }

            UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>();

            if (user == null)
                return Result.Failure<UserDTO?>(new Error(500, "Invalid response from Users service"));

            return Result.Success<UserDTO?>(user);
        }
    }
}
