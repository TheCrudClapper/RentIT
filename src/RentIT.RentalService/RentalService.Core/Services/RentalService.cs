using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Mappings;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalValidator _rentalValidator;
        private readonly IEquipmentMicroserviceClient _equipmentMicroserviceClient;
        public RentalService(IRentalRepository rentalRepository,
            IRentalValidator rentalValidator,
            IEquipmentMicroserviceClient equipmentMicroserviceClient)
        {
            _rentalRepository = rentalRepository;
            _rentalValidator = rentalValidator;
            _equipmentMicroserviceClient = equipmentMicroserviceClient;
        }

        public async Task<Result<RentalResponse>> AddRental(RentalAddRequest request)
        {
            Rental rental = request.ToRentalEntity();

            var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId);
            if (equipmentResponse.IsFailure)
                return Result.Failure<RentalResponse>(equipmentResponse.Error);

            var validationResult = await _rentalValidator.ValidateEntity(rental, equipmentResponse.Value);

            if (validationResult.IsFailure)
                return Result.Failure<RentalResponse>(validationResult.Error);

            //Calculating Total Rental Price
            rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
                rental.EndDate,
                equipmentResponse.Value.RentalPricePerDay);

            Rental newRental = await _rentalRepository.AddRentalAsync(rental);            
            return newRental.ToRentalResponse(equipmentResponse.Value);
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
            Rental? rental = await _rentalRepository.GetRentalByIdAsync(rentalId);
            if (rental == null)
                return Result.Failure<RentalResponse>(RentalErrors.RentalNotFound);

            var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId);
            if (equipmentResponse.IsFailure)
                return Result.Failure<RentalResponse>(equipmentResponse.Error);

            return rental.ToRentalResponse(equipmentResponse.Value);
        }

        public async Task<Result<IEnumerable<RentalResponse>>> GetAllRentals()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();

            var equipmentIds = rentals.Select(x => x.EquipmentId).Distinct();

            var eqResponse = await _equipmentMicroserviceClient.GetEquipmentsByIds(equipmentIds);
            if (eqResponse.IsFailure)
                return Result.Failure<IEnumerable<RentalResponse>>(eqResponse.Error);

            var equipmentDict = eqResponse.Value.ToDictionary(x => x.Id, x => x);

            return rentals
            .Where(r => equipmentDict.ContainsKey(r.EquipmentId))
            .Select(r => r.ToRentalResponse(equipmentDict[r.EquipmentId]))
            .ToList();
        }

        public async Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request)
        {
            Rental rental = request.ToRentalEntity();

            var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId);
            if (equipmentResponse.IsFailure)
                return Result.Failure<RentalResponse>(equipmentResponse.Error);

            var validationResult = await _rentalValidator.ValidateEntity(rental, equipmentResponse.Value);

            if (validationResult.IsFailure)
                return Result.Failure(validationResult.Error);

            rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
                rental.EndDate,
                equipmentResponse.Value.RentalPricePerDay);

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

        public async Task<Result> DeleteRentalByEquipmentId(Guid equipmentId)
        {
            bool isSuccess = await _rentalRepository.DeleteRentalsByEquipmentAsync(equipmentId);

            if (!isSuccess)
                return Result.Failure(RentalErrors.FailedToDeleteRelatedRentals);

            return Result.Success();
        }
    }
}
