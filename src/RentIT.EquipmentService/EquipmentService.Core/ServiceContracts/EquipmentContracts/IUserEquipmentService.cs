using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.Equipment;

/// <summary>
/// Defines operations for managing user-associated equipment, including adding, updating, retrieving, and deleting
/// equipment records.
/// </summary>
/// <remarks>This interface provides asynchronous methods for handling equipment data linked to users.
/// Implementations are expected to enforce validation and authorization as appropriate. All methods accept a
/// cancellation token to support cooperative cancellation of ongoing operations.</remarks>
public interface IUserEquipmentService
{
    Task<Result<EquipmentResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<EquipmentResponse>> GetAllUserEquipment(Guid userId, CancellationToken cancellationToken);
    Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId, CancellationToken cancellationToken);
    Task<Result<EquipmentResponse>> GetUserEquipmentById(Guid userId, Guid equipmentId, CancellationToken cancellationToken);
}
