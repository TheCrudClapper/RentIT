using EquipmentService.Core.Domain.Entities;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository : IBaseEquipmentRepository
{ 
    Task<Equipment> AddEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
    Task<Equipment?> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment, CancellationToken cancellationToken);
    Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<IEnumerable<Equipment>> GetAllEquipmentAsync(CancellationToken cancellationToken);
    Task DeleteEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);
}

