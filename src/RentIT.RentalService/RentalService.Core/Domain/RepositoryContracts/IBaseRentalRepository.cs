using RentalService.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RentalService.Core.Domain.RepositoryContracts;

public interface IBaseRentalRepository
{
    Task<IEnumerable<Rental>> GetRentalsByCondition(Expression<Func<Rental, bool>> conditionExpression);
    Task<Rental?> GetRentalByCondition(Expression<Func<Rental, bool>> conditionExpression);
}

