using Microsoft.Extensions.Caching.Distributed;
using Polly;
using Polly.CircuitBreaker;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.ResultTypes;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RentalService.Infrastructure.HttpClients
{
    public class EquipmentMicroserviceClient : IEquipmentMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IEquipmentMicroservicePolicies _eqPolicies;
        private readonly IDistributedCache _distributedCache;
        public EquipmentMicroserviceClient(HttpClient htppClient, IEquipmentMicroservicePolicies eqPolicies,
            IDistributedCache distributedCache)
        {
            _httpClient = htppClient;
            _eqPolicies = eqPolicies;
            _distributedCache = distributedCache;
        }

        public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            try
            {
                string cacheKey = $"equipment:{equipmentId}";
                string? cachedEquipment = await _distributedCache.GetStringAsync(cacheKey);

                if (cachedEquipment != null)
                {

                    EquipmentResponse? cachedObject = JsonSerializer.Deserialize<EquipmentResponse>(cachedEquipment);
                    return cachedObject;
                }

                HttpResponseMessage response = await _httpClient.GetAsync($"/api/equipments/{equipmentId}");

                if (!response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return Result.Failure<EquipmentResponse>(new Error((int)response.StatusCode, message));
                }

                EquipmentResponse? details = await response.Content.ReadFromJsonAsync<EquipmentResponse>();

                if (details == null)
                    return Result.Failure<EquipmentResponse>(new Error(500, "Invalid response from Equipment service"));

                var obj = JsonSerializer.Serialize(details);
                var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));

                string cacheKeyToWrite = $"equipment:{equipmentId}";

                await _distributedCache.SetStringAsync(cacheKeyToWrite, obj, cacheOptions);

                //Key: equipment:{equipmentId}
                //Value: { "Name", etc..}

                return details;

            }
            catch (BrokenCircuitException)
            {
                return Result.Failure<EquipmentResponse>(new Error(503, "Service unavaliable, try again later"));
            }
        }

        //TO DO : IMPLEMENT CACHING
        public async Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds)
        {
            //To Do: read from cache here

            var fallbackPolicy = _eqPolicies.GetFallbackPolicyForEquipmentsByIds();
            {
                var context = new Context();
                context["equipmentIds"] = equipmentIds;

                HttpResponseMessage response = await fallbackPolicy.ExecuteAsync(
                  async (ctx) => await _httpClient.PostAsJsonAsync("api/equipments/byIds", equipmentIds), context
                );


                //HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/equipments/byIds", equipmentIds);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        IEnumerable<EquipmentResponse>? equipmentItemsFromFallback = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>();

                        return Result.Success(equipmentItemsFromFallback ?? Enumerable.Empty<EquipmentResponse>());
                    }
                    string message = await response.Content.ReadAsStringAsync();
                    return Result.Failure<IEnumerable<EquipmentResponse>>(new Error((int)response.StatusCode, message));
                }
                //TO DO: save to cache here


                IEnumerable<EquipmentResponse>? equipmentItems = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>();

                return Result.Success(equipmentItems ?? Enumerable.Empty<EquipmentResponse>());
            }
        }
    }
}