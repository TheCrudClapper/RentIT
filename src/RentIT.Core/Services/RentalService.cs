using RentIT.Core.CustomValidators;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Core.DTO.RentalDto;
using RentIT.Core.Mappings;
using RentIT.Core.ResultTypes;
using RentIT.Core.ServiceContracts;

namespace RentIT.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly RentalValidator _rentalValidator;
        public RentalService(IRentalRepository rentalRepository, RentalValidator rentalValidator)
        {
            _rentalRepository = rentalRepository;
            _rentalValidator = rentalValidator;
        }

        public async Task<Result<RentalResponse>> AddRental(RentalAddRequest request)
        {
            Rental rental = request.ToRentalEntity();

            Rental newRental = await _rentalRepository.AddRentalAsync(rental);

            return newRental.ToRentalResponse();
        }

        public async Task<Result> DeleteRental(Guid rentalId)
        {
            bool isSuccess = await _rentalRepository.DeleteRentalAsync(rentalId);

            if (!isSuccess)
                return Result.Failure(RentalErrors.RentalNotFound);

            return Result.Success();
        }

        public async Task<Result<RentalResponse>> GetActiveRental(Guid rentalId)
        {
            Rental? rental = await _rentalRepository.GetActiveRentalByIdAsync(rentalId);
            if (rental == null)
                return Result.Failure<RentalResponse>(RentalErrors.RentalNotFound);

            return rental.ToRentalResponse();
        }

        public async Task<IEnumerable<RentalResponse>> GetAllActiveRentals()
        {
            IEnumerable<Rental> rentals = await _rentalRepository.GetAllActiveRentalsAsync();
            return rentals.Select(item => item.ToRentalResponse());
        }

        public async Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request)
        {
            Rental rental = request.ToRentalEntity();

            bool isSuccess = await _rentalRepository.UpdateRentalAsync(rentalId, rental);

            if (!isSuccess)
                return Result.Failure(RentalErrors.RentalNotFound);

            return Result.Success();
        }

        //private async Task<Result> ValidateRentalRequests()
        //{

        //}
    }
}
