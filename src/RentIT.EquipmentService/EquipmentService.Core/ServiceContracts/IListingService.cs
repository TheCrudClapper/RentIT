using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.Equipments.Admin;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.ServiceContracts;

public interface IListingService
{
    Task<Result<UpdatedResponse>> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request);
    Task<Result<CreatedResponse>> AddEquipment(EquipmentAddRequest request);
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentItems(CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken = default);
    Task<Result> DeleteListing(Guid id);
}
