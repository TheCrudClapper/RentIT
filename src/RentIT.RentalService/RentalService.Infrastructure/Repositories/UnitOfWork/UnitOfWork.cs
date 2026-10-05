using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly RentalDbContext _context;
    public UnitOfWork(RentalDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _context.SaveChangesAsync(ct);
}
