using Microsoft.Extensions.Configuration;
using RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;
using RentIT.UI.Contracts.DTO.Shared;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;

namespace RentIT.UI.Infrastructure.HttpClients;

public class UserEquipmentHttpClient : HttpClientBase, IUserEquipmentHttpClient
{
    public UserEquipmentHttpClient(HttpClient httpClient, IConfiguration config) : base(httpClient, config) { }
    public async Task<Result> DeleteEquipment(Guid id)
        => await DeleteAsync($"user/equipments/{id}");

    public async Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList()
        => await GetAsync<IReadOnlyCollection<UserEquipmentListResponse>>("user/equipments");

    public async Task<Result<UserEquipmentResponse>> GetEquipment(Guid id)
        => await GetAsync<UserEquipmentResponse>($"user/equipments/{id}");

    public async Task<Result<CreatedResponse>> PostEquipment(UserEquipmentAddRequest request)
        => await PostAsync<CreatedResponse>("$user/equipments", request);
    public async Task<Result<UpdatedResponse>> PutEquipment(Guid id, UserEquipmentUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"user/equipments/{id}", request);
}
