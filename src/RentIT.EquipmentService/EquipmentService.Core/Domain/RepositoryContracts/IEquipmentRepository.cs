using EquipmentService.Core.Domain.Entities;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository : IBaseEquipmentRepository
{ 
    Task<Equipment> AddEquipmentAsync(Equipment equipment);
    Task<bool> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment);
    Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId);
    Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
    Task DeleteEquipmentAsync(Equipment equipment);
}

