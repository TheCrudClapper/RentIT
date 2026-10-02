namespace RentalService.Core.Domain.RepositoryContracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
