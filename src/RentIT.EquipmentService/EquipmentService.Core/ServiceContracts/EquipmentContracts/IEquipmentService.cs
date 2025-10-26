using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.Equipment;

public interface IEquipmentService
{
    Task UpdateEquipmentRating(Guid equipmentId, decimal rating, decimal? oldRating,  CancellationToken cancellationToken = default);
    Task<Result> UpdateEquipment(Guid equipmentId,EquipmentUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request, CancellationToken cancellationToken = default);
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems(CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentResponse>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken = default);
    Task<Result> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken = default);
}
