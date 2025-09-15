using Polly;
using Polly.CircuitBreaker;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.ResultTypes;
using System.Net.Http.Json;

namespace RentalService.Infrastructure.HttpClients
{
    public class EquipmentMicroserviceClient : IEquipmentMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IRentalMicroservicePolicies _policies;
        public EquipmentMicroserviceClient(HttpClient htppClient, IRentalMicroservicePolicies policies)
        {
            _httpClient = htppClient;
            _policies = policies;
        }

        public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            //local circuit breaker policy
            var policy = _policies.GetCircuitBreakerPolicy();
            try
            {
                HttpResponseMessage response = await policy.ExecuteAsync(async () =>
                {
                   return await _httpClient.GetAsync($"/api/equipments/{equipmentId}");
                }); 

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
            catch (BrokenCircuitException)
            {
                return Result.Failure<EquipmentResponse>(new Error(503, "Service unavaliable, try again later"));
            }
        }

        public async Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds)
        {
            var context = new Context();
            context["equipmentIds"] = equipmentIds;

            var combinedPolicy = _policies.GetCircuitBreakerWithFallbackPolicy();

            HttpResponseMessage response = await combinedPolicy.ExecuteAsync(
              async (ctx) => await _httpClient.PostAsJsonAsync("api/equipments/byIds", equipmentIds),context
            );

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
