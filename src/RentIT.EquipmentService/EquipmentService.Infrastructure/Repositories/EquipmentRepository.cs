using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;

namespace EquipmentService.Infrastructure.Repositories;

public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(EquipmentContext context) : base(context) { }
}
