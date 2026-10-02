using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EquipmentContext _context;
    public UnitOfWork(EquipmentContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct)
        => _context.SaveChangesAsync(ct);
}
