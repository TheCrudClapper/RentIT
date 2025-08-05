using RentIT.Core.Domain.Entities;
namespace RentIT.Core.Domain.RepositoryContracts
{
    public interface IEquipmentRepository
    {
        Task<Equipment?> GetActiveEquipmentByIdAsync(Guid equipmentId);
        Task<IEnumerable<Equipment>> GetAllActiveEquipmentItemsAsync();
        Task<bool> DeleteEquipmentAsync(Guid equipmentId);
    }
}
