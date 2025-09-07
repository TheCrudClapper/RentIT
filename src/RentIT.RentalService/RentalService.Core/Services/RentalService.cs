using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Mappings;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Validators.Contracts;
using System.ComponentModel.DataAnnotations;

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

            var validationResult = await _rentalValidator.ValidateNewEntity(rental, equipmentResponse.Value);

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

        //WIP
        public async Task<IEnumerable<RentalResponse>> GetAllRentals()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();

            var response = new List<RentalResponse>();

            foreach(var rental in rentals)
            {
                var eqResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId);
                if (eqResponse.IsFailure)
                    continue;

                response.Add(rental.ToRentalResponse(eqResponse.Value));
            }

            return response;
        }

        //WIP
        public async Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request)
        {
            Rental rental = request.ToRentalEntity();

            var validationResult = await _rentalValidator.ValidateUpdateEntity(rental, rentalId);

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
    }
}
