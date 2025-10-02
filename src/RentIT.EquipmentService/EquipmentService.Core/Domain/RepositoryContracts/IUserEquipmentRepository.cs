using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IUserEquipmentRepository : IBaseEquipmentRepository
{
    Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId, CancellationToken cancellationToken);
    Task<Equipment?> UpdateUserEquipmentAsync(Guid equipmentId, Equipment equipment, CancellationToken cancellationToken);
    Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId, CancellationToken cancellationToken);
    Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId, CancellationToken cancellationToken);
    Task DeleteUserEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
}
