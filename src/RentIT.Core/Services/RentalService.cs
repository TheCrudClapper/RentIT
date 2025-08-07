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
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        public RentalService(IRentalRepository rentalRepository, IEquipmentRepository equipmentRepository, IUserRepository userRepository)
        {
            _rentalRepository = rentalRepository;
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<RentalResponse>> AddRental(RentalAddRequest request)
        {
            Rental rental = request.ToRentalEntity();
            
            var validationResult = await ValidateRentalEntity(rental);

            if (validationResult.IsFailure)
                return Result.Failure<RentalResponse>(validationResult.Error);

            var dailyPrice = await _equipmentRepository.GetDailyPriceAsync(rental.EquipmentId);
            if (dailyPrice == null)
                return Result.Failure<RentalResponse>(EquipmentErrors.EquipmentNotFound);

            rental.TotalRentalPrice = CalculateTotalRentalPrice(rental.StartDate, rental.EndDate, dailyPrice.Value);

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

        public async Task<Result<RentalResponse>> GetRental(Guid rentalId)
        {
            Rental? rental = await _rentalRepository.GetActiveRentalByIdAsync(rentalId);
            if (rental == null)
                return Result.Failure<RentalResponse>(RentalErrors.RentalNotFound);

            return rental.ToRentalResponse();
        }

        public async Task<IEnumerable<RentalResponse>> GetAllRentals()
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

        private decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, decimal dailyPrice)
        {
            var days = (endDate.Date - startDate.Date).Days;
            return dailyPrice * days;
        }

        private async Task<Result> ValidateRentalEntity(Rental entity)
        {
            //Check given equipmentId
            if (!await _equipmentRepository.DoesEquipmentExistsAsync(entity.EquipmentId))
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            //Check given userId
            if(!await _userRepository.DoesUserExistsAsync(entity.RentedByUserId))
                return Result.Failure(UserErrors.UserNotFound);

            //Check avaliablitiy of item
            var equipmentStatus = await _equipmentRepository.GetEquipmentStatusAsync(entity.EquipmentId);

            if(equipmentStatus == null 
                || equipmentStatus == RentStatusEnum.Maintenance 
                || equipmentStatus == RentStatusEnum.Rented)
                return Result.Failure(EquipmentErrors.EquipmentNotAvaliable);

            //Check ownership of item
            if (await _equipmentRepository.DoesEquipmentBelongsToUser(entity.EquipmentId, entity.RentedByUserId))
                return Result.Failure(RentalErrors.RentalForSelfEquipment);

            return Result.Success();
        }
    }
}
