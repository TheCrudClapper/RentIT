using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.Equipments;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.ServiceContracts.Equipment;
/// <summary>
/// Defines operations for managing equipment entities, including creation, retrieval, update, deletion, and rating
/// management.
/// </summary>
/// <remarks>This interface provides asynchronous methods for handling equipment data and ratings. Implementations
/// are expected to support cancellation via the provided cancellation tokens. Methods return results indicating success
/// or failure, and some return detailed equipment information. Thread safety and transactional guarantees depend on the
/// specific implementation.</remarks>
public interface IEquipmentService
{
    Task UpdateEquipmentRating(Guid equipmentId, decimal rating, decimal? oldRating = null);
    Task DeleteEquipmentRating(Guid equipmentId, decimal rating);
    Task<Result<UpdatedResponse>> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request);
    Task<Result<CreatedResponse>> AddEquipment(EquipmentAddRequest request);
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentItems(CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken = default);
    Task<Result> DeleteEquipment(Guid equipmentId);
    Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken = default);
}
