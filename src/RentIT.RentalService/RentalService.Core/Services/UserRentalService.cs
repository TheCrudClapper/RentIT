using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Services
{
    public class UserRentalService : IUserRentalService
    {
        private readonly IUserRentalRepository _userRentalRepository;
        private readonly IUserRentalValidator _userRentalValidator;
        public UserRentalService(IUserRentalRepository userRentalRepository, IUserRentalValidator validator)
        {
            _userRentalRepository = userRentalRepository;
            _userRentalValidator = validator;
        }
        public async Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId)
        {
            //var rental = request.ToRental();
            //rental.UserId = userId;

            //var validationResult = await _userRentalValidator.ValidateNewEntity(rental);

            //if (validationResult.IsFailure)
            //    return Result.Failure<RentalResponse>(validationResult.Error);

            throw new NotImplementedException();
                
        }

        public Task<Result> DeleteRental(Guid rentalId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RentalResponse>> GetAllRentals(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
