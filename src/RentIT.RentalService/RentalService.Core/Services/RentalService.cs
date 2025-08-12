using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Mappings;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;

namespace RentIT.Core.Services
{
    //DISCLAIMER !
    //Commented out ,for now, unused code or deprecated one. Will figure how to communicate with other apis soon
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<RentalResponse>> AddRental(RentalAddRequest request)
        {
            Rental rental = request.ToRentalEntity();
            
            //var validationResult = await ValidateAddRentalEntity(rental);

            //if (validationResult.IsFailure)
            //    return Result.Failure<RentalResponse>(validationResult.Error);

            //var dailyPrice = await _equipmentRepository.GetDailyPriceAsync(rental.EquipmentId);
            //if (dailyPrice == null)
            //    return Result.Failure<RentalResponse>(EquipmentErrors.EquipmentNotFound);

            //rental.TotalRentalPrice = CalculateTotalRentalPrice(rental.StartDate, rental.EndDate, dailyPrice.Value);

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

            var validationResult = await ValidateUpdateRentalEntity(rental);

            if (validationResult.IsFailure)
                return Result.Failure(validationResult.Error);

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

        //private async Task<Result> ValidateAddRentalEntity(Rental entity)
        //{
        //    var equipment = await _equipmentRepository.GetActiveEquipmentByIdAsync(entity.EquipmentId);

        //    //Check nullability of equipment from request
        //    if (equipment == null)
        //        return Result.Failure(EquipmentErrors.EquipmentNotFound);

        //    //Check if user exists
        //    if (!await _userRepository.DoesUserExistsAsync(entity.RentedByUserId))
        //        return Result.Failure(UserErrors.UserNotFound);

        //    //Check ownership of item
        //    if (equipment.CreatedByUserId != entity.RentedByUserId)
        //        return Result.Failure(RentalErrors.RentalForSelfEquipment);

        //    //Status check on equipment
        //    if (equipment.Status == RentStatusEnum.Maintenance)
        //        return Result.Failure(EquipmentErrors.EquipmentInMaintnance);

        //    if(equipment.Status == RentStatusEnum.Rented)
        //        return Result.Failure(EquipmentErrors.EquipmentRented(entity.StartDate, entity.EndDate));

        //    return Result.Success();
        //}

        private async Task<Result> ValidateUpdateRentalEntity(Rental entity)
        {
            //Check given equipmentId
            //if (!await _equipmentRepository.DoesEquipmentExistsAsync(entity.EquipmentId))
            //    return Result.Failure(EquipmentErrors.EquipmentNotFound);
            
            //validate ownership, for now skip it, we dont have authentication or authorization


            return Result.Success();
        }

    }
}
