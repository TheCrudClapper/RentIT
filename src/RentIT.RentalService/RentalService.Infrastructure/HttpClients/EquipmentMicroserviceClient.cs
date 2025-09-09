using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;
using System.Net.Http.Json;

namespace RentalService.Infrastructure.HttpClients
{
    public class EquipmentMicroserviceClient : IEquipmentMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public EquipmentMicroserviceClient(HttpClient htppClient)
        {
            _httpClient = htppClient;
        }
        public async Task<Result<bool>> DoesEquipmentExist(Guid equipmentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/equipments/exists/{equipmentId}");

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Result.Failure<bool>(new Error((int)response.StatusCode, message));
            }

            if (response.Content == null)
                return false;

            return true;
        }

        public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/equipments/{equipmentId}");

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Result.Failure<EquipmentResponse>(new Error((int)response.StatusCode, message));
            }

            EquipmentResponse? details = await response.Content.ReadFromJsonAsync<EquipmentResponse>();

            if (details == null)
                return Result.Failure<EquipmentResponse>(new Error(500, "Invalid response from Equipment service"));

            return details;
        }

        public async Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/equipments/byIds", equipmentIds);

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Result.Failure<IEnumerable<EquipmentResponse>>(new Error((int)response.StatusCode, message));
            }

            IEnumerable<EquipmentResponse>? equipmentItems = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>();

            return Result.Success(equipmentItems ?? Enumerable.Empty<EquipmentResponse>());
        }
    }
}
