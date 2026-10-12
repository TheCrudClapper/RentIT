using RentIT.Contracts.DTO.EquipmentMicroservice.Equipments;
using RentIT.Contracts.DTO.Shared;
using RentIT.UI.Core.ResultTypes;
namespace RentIT.UI.Core.HttpClientContracts;

public interface IUserEquipmentHttpClient
{
    Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList();
    Task<Result> DeleteEquipment(Guid id);
    Task<Result<UserEquipmentResponse>> GetEquipment(Guid id);
    Task<Result<UpdatedResponse>> PutEquipment(Guid id, UserEquipmentUpdateRequest request);
    Task<Result<CreatedResponse>> PostEquipment(UserEquipmentAddRequest request);
}
