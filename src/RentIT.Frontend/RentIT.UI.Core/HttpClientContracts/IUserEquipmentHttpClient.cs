using EquipmentService.Core.DTO.Equipments;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.HttpClientContracts;

public interface IUserEquipmentHttpClient 
{
    Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList();
    Task<Result> DeleteEquipment(Guid id);
    Task<Result<EquipmentResponse>> GetEquipment(Guid id);
    Task<Result> PutEquipment(Guid id, EquipmentUpdateRequest request);
}
