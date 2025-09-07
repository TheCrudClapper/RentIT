using RentalService.Core.Domain.Entities;
using RentalService.Core.ResultTypes;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Validators.Implementations
{
    public class UserRentalValidator : IUserRentalValidator
    {
        public Task<Result> ValidateNewEntity(Rental entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ValidateUpdateEntity(Rental entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
