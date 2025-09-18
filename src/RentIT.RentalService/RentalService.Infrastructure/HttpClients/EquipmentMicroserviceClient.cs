using Microsoft.Extensions.Caching.Distributed;
using Polly;
using Polly.CircuitBreaker;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.ResultTypes;
using RentalService.Infrastructure.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RentalService.Infrastructure.HttpClients
{
    public class EquipmentMicroserviceClient : IEquipmentMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IEquipmentMicroservicePolicies _eqPolicies;
        private readonly CachingHelper _cachingHelper;
        private readonly IDistributedCache _distributedCache;
        public EquipmentMicroserviceClient(HttpClient htppClient, IEquipmentMicroservicePolicies eqPolicies,
            IDistributedCache distributedCache,
            CachingHelper cachingHelper)
        {
            _httpClient = htppClient;
            _eqPolicies = eqPolicies;
            _distributedCache = distributedCache;
            _cachingHelper = cachingHelper;
        }

        public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            try
            {
                string cacheKey = CachingHelper.GenerateCacheKey("equipment", equipmentId);

                var cachedObj = await _cachingHelper.GetCachedObject<EquipmentResponse>(cacheKey);
                if (cachedObj.Value != null)
                    return cachedObj.Value;

                HttpResponseMessage response = await _httpClient.GetAsync($"/api/equipments/{equipmentId}");

                if (!response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return Result.Failure<EquipmentResponse>(new Error((int)response.StatusCode, message));
                }

                EquipmentResponse? details = await response.Content.ReadFromJsonAsync<EquipmentResponse>();

                if (details == null)
                    return Result.Failure<EquipmentResponse>(new Error(500, "Invalid response from Equipment service"));

                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));

                await _cachingHelper.CacheObject<EquipmentResponse>(details, cacheKey, cacheOptions);

                return details;

            }
            catch (BrokenCircuitException)
            {
                return Result.Failure<EquipmentResponse>(new Error(503, "Service unavaliable, try again later"));
            }
        }

        public async Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds)
        {
            var cacheKey = CachingHelper.GenerateCacheKey("equipments", equipmentIds);
            var cachedObj = await _cachingHelper.GetCachedObject<IEnumerable<EquipmentResponse>>(cacheKey);
            if (cachedObj.Value != null)
                return Result.Success(cachedObj.Value);

            var fallbackPolicy = _eqPolicies.GetFallbackPolicyForEquipmentsByIds();

            var context = new Context();
            context["equipmentIds"] = equipmentIds;

            HttpResponseMessage response = await fallbackPolicy.ExecuteAsync(
              async (ctx) => await _httpClient.PostAsJsonAsync("api/equipments/byIds", equipmentIds), context
            );

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    IEnumerable<EquipmentResponse>? equipmentItemsFromFallback = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>();

                    return Result.Success(equipmentItemsFromFallback ?? []);
                }

                string message = await response.Content.ReadAsStringAsync();
                return Result.Failure<IEnumerable<EquipmentResponse>>(new Error((int)response.StatusCode, message));
            }

            IEnumerable<EquipmentResponse>? equipmentItems = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>();

            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1000))
                .SetSlidingExpiration(TimeSpan.FromSeconds(1000));

            await _cachingHelper.CacheObject(equipmentItems, cacheKey, cacheOptions);

            return Result.Success(equipmentItems ?? []);

        }
    }
}