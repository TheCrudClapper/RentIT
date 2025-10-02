using Polly;
using Polly.CircuitBreaker;
using RentalService.Core.Caching;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Policies.Contracts;
using RentalService.Core.ResultTypes;
using System.Net;
using System.Net.Http.Json;

namespace RentalService.Infrastructure.HttpClients;

public class EquipmentMicroserviceClient : IEquipmentMicroserviceClient
{
    private readonly HttpClient _httpClient;
    private readonly IEquipmentMicroservicePolicies _eqPolicies;
    private readonly ICachingHelper _cachingHelper;
    public EquipmentMicroserviceClient(HttpClient htppClient, IEquipmentMicroservicePolicies eqPolicies,
        ICachingHelper cachingHelper)
    {
        _httpClient = htppClient;
        _eqPolicies = eqPolicies;
        _cachingHelper = cachingHelper;
    }

    public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        try
        {
            string cacheKey = CachingHelper.GenerateCacheKey("equipment", equipmentId);

            var cachedObj = await _cachingHelper.GetCachedObject<EquipmentResponse>(cacheKey, cancellationToken);
            if (cachedObj.Value != null)
                return cachedObj.Value;

            HttpResponseMessage response = await _httpClient.GetAsync($"/gateway/equipments/{equipmentId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync(cancellationToken);
                return Result.Failure<EquipmentResponse>(new Error((int)response.StatusCode, message));
            }

            EquipmentResponse? details = await response.Content.ReadFromJsonAsync<EquipmentResponse>(cancellationToken);

            if (details == null)
                return Result.Failure<EquipmentResponse>(new Error(500, "Invalid response from Equipment service"));

            await _cachingHelper.CacheObject(details, cacheKey, CachingProfiles.ShortTTLCacheOption, cancellationToken);

            return details;

        }
        catch (BrokenCircuitException)
        {
            return Result.Failure<EquipmentResponse>(new Error(503, "Service unavaliable, try again later"));
        }
    }

    public async Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken)
    {
        var cacheKey = CachingHelper.GenerateCacheKey("equipments", equipmentIds);
        var cachedObj = await _cachingHelper.GetCachedObject<IEnumerable<EquipmentResponse>>(cacheKey, cancellationToken);
        if (cachedObj.Value != null)
            return Result.Success(cachedObj.Value);

        var fallbackPolicy = _eqPolicies.GetFallbackPolicyForEquipmentsByIds();

        var context = new Context();
        context["equipmentIds"] = equipmentIds;

        HttpResponseMessage response = await fallbackPolicy.ExecuteAsync(
          async (ctx, token) => await _httpClient.PostAsJsonAsync("gateway/equipments/byIds", equipmentIds), context, cancellationToken
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

        IEnumerable<EquipmentResponse>? equipmentItems = await response.Content.ReadFromJsonAsync<IEnumerable<EquipmentResponse>>(cancellationToken);

        await _cachingHelper.CacheObject(equipmentItems, cacheKey, CachingProfiles.ShortTTLCacheOption, cancellationToken);

        return Result.Success(equipmentItems ?? []);

    }
}