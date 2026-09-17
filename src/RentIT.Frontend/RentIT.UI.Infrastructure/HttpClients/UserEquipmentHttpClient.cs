using EquipmentService.Core.DTO.Equipments;
using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.DTO.Shared;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;

namespace RentIT.UI.Infrastructure.HttpClients;

public class UserEquipmentHttpClient : HttpClientBase, IUserEquipmentHttpClient
{
    public UserEquipmentHttpClient(HttpClient httpClient, IConfiguration config) : base(httpClient, config) { }

    public async Task<Result<CreatedResponse>> PostEquipment(EquipmentAddRequest request)
         => await PostAsync<CreatedResponse>("$user/equipments", request);

    public async Task<Result> DeleteEquipment(Guid id)
        => await DeleteAsync($"user/equipments/{id}");

    public async Task<Result<EquipmentResponse>> GetEquipment(Guid id)
        => await GetAsync<EquipmentResponse>($"user/equipments/{id}");

    public async Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList()
        => await GetAsync<IReadOnlyCollection<UserEquipmentListResponse>>("user/equipments");

    public async Task<Result<UpdatedResponse>> PutEquipment(Guid id, EquipmentUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"user/equipments/{id}", request);
}
