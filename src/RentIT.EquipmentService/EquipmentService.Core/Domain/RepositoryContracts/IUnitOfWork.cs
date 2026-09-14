namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
