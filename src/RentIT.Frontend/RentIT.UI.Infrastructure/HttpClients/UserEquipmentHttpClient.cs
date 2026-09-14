using EquipmentService.Core.DTO.Equipments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.DTO.Shared;
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

    public async Task<Result<CreatedResponse>> CreateEquipment(EquipmentAddRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"user/equipments", request);

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails is null)
                return Result.Failure<CreatedResponse>(Error.Create("Failed", "Something went wrong."));

            return Result.Failure<CreatedResponse>(Error.Create(problemDetails.Title, problemDetails.Detail));
        }

        var created = await response.Content.ReadFromJsonAsync<CreatedResponse>();
        if (created is null)
            return Result.Failure<CreatedResponse>(Error.Create("Failed", "Response was not recieved."));

        return created;
    }

    public async Task<Result> DeleteEquipment(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"user/equipments/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails is null)
                return Result.Failure(Error.Create("Error occured.", "Something went wrong."));

            return Result.Failure(Error.Create(problemDetails.Title, problemDetails.Detail));
        }
            
        return Result.Success();
    }

    public async Task<Result<EquipmentResponse>> GetEquipment(Guid id)
    {
        var response = await _httpClient.GetAsync($"user/equipments/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails is null)
                return Result.Failure<EquipmentResponse>(Error.Create("Failed", "Something went wrong."));

            return Result.Failure<EquipmentResponse>(Error.Create(problemDetails.Title, problemDetails.Detail));
        }

        var equipment = await response.Content.ReadFromJsonAsync<EquipmentResponse>();
        if (equipment is null)
            return Result.Failure<EquipmentResponse>(Error.Create("Failed", "Equipment was not recieved."));

        return equipment;
    }

    public async Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList()
    {
        var response = await _httpClient.GetAsync("user/equipments");

        response.EnsureSuccessStatusCode();

        var equipments = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<UserEquipmentListResponse>>() ?? [];

        return Result.Success(equipments);
    }

    public async Task<Result<UpdatedResponse>> PutEquipment(Guid id, EquipmentUpdateRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"user/equipments/{id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails is null)
                return Result.Failure<UpdatedResponse>(Error.Create("Failed", "Something went wrong."));

            return Result.Failure<UpdatedResponse>(Error.Create(problemDetails.Title, problemDetails.Detail));
        }

        var updated = await response.Content.ReadFromJsonAsync<UpdatedResponse>();
        if(updated is null)
            return Result.Failure<UpdatedResponse>(Error.Create("Failed", "Response was not recieved."));

        return updated;
    }


}
