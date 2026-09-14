using EquipmentService.Core.Domain.Entities.Equipments;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository : IBaseEquipmentRepository, IGenericRepository<Equipment>
{
    Task UpdateEquipmentRating(Equipment equipment, decimal newAverageRating, int reviewCountToAdd = 0);
}

