using RentalService.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RentalService.Core.Domain.RepositoryContracts;
/// <summary>
/// Defines methods for retrieving rental entities based on specified conditions.
/// </summary>
/// <remarks>This interface provides query operations for rental data using expression predicates, enabling
/// flexible filtering scenarios. Implementations are expected to support asynchronous operations and cancellation via
/// the provided token.</remarks>
public interface IBaseRentalRepository
{
    Task<IEnumerable<Rental>> GetRentalsByCondition(Expression<Func<Rental, bool>> conditionExpression, CancellationToken cancellationToken);
    Task<Rental?> GetRentalByCondition(Expression<Func<Rental, bool>> conditionExpression, CancellationToken cancellationToken);
}

