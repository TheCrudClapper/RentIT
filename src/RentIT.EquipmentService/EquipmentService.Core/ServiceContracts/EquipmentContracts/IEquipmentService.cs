using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.Equipment;

public interface IEquipmentService
{
    Task<Result> UpdateEquipment(Guid equipmentId,EquipmentUpdateRequest request, CancellationToken cancellationToken);
    Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request, CancellationToken cancellationToken);
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken);
    Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems(CancellationToken cancellationToken);
    Task<IEnumerable<EquipmentResponse>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken);
    Task<Result> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken);
    Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken);
}
