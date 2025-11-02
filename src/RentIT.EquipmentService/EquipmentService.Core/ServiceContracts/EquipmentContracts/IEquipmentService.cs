using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

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
    Task UpdateEquipmentRating(Guid equipmentId, decimal rating, decimal? oldRating = null,  CancellationToken cancellationToken = default);
    Task DeleteEquipmentRating(Guid equipmentId, decimal rating, CancellationToken cancellationToken = default);
    Task<Result> UpdateEquipment(Guid equipmentId,EquipmentUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request, CancellationToken cancellationToken = default);
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<EquipmentResponse>>> GetAllEquipmentItems(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<EquipmentResponse>>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken = default);
    Task<Result> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken = default);
}
