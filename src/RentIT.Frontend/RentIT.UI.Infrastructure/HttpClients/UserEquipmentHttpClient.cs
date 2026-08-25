using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;
using System.Net.Http.Json;

namespace RentIT.UI.Infrastructure.HttpClients;

public class UserEquipmentHttpClient : HttpClientBase, IUserEquipmentHttpClient
{
    public UserEquipmentHttpClient(HttpClient httpClient, IConfiguration config) : base(httpClient, config)
    {
    }

    public async Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList()
    {
        var response = await _httpClient.GetAsync("user/equipments");

        response.EnsureSuccessStatusCode();

        var equipments = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<UserEquipmentListResponse>>() ?? [];

        return Result.Success(equipments);
    }
}
