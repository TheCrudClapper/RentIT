using EquipmentService.Core.Domain.Entities;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository : IBaseEquipmentRepository
{ 
    Task<Equipment> AddEquipmentAsync(Equipment equipment, CancellationToken cancellationToken = default);
    Task<Equipment?> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment, CancellationToken cancellationToken = default);
    Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Equipment>> GetAllEquipmentAsync(CancellationToken cancellationToken = default);
    Task DeleteEquipmentAsync(Equipment equipment, CancellationToken cancellationToken = default);
    Task UpdateEquipmentRating(Equipment equipment, decimal newAverageRating, int reviewCountToAdd = 0);
}

