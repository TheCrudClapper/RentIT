using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Validators.Implementations
{
    public class RentalValidator : IRentalValidator
    {
        private readonly IUsersMicroserviceClient _usersMicroserviceClient;
        private readonly IRentalRepository _rentalRepository;
        public RentalValidator(IUsersMicroserviceClient usersMicroserviceClient,
            IRentalRepository rentalRepository,
            IEquipmentMicroserviceClient equipmentMicroserviceClient)
        {
            _usersMicroserviceClient = usersMicroserviceClient;
            _rentalRepository = rentalRepository;
        }

        public Task<Result> ValidateNewEntity(Rental entity) =>
            ValidateRental(entity);

        public Task<Result> ValidateNewEntity(Rental entity, EquipmentResponse equipmentResponse) =>
            ValidateRental(entity, equipmentResponse);

        public Task<Result> ValidateUpdateEntity(Rental entity, Guid rentalId) =>
            ValidateRental(entity);

        public Task<Result> ValidateUpdateEntity(Rental entity, Guid rentalId, EquipmentResponse equipmentResponse) =>
            ValidateRental(entity, equipmentResponse);

        public async Task<Result> ValidateRental(Rental entity,
            EquipmentResponse? equipmentResponse = null)
        {
            var userValidation = await ValidateUser(entity.UserId);
            if (userValidation.IsFailure)
                return userValidation;

            if (equipmentResponse is not null && entity.UserId == equipmentResponse.CreatedByUserId)
                return Result.Failure(RentalErrors.RentalForSelfEquipment);

            return await ValidateRentalPeriod(entity);
        }

        //Private Helper Methods
        private async Task<Result> ValidateRentalPeriod(Rental enitity)
        {
            var conflicts = await _rentalRepository.GetRentalsByCondition(item => item.EquipmentId == enitity.EquipmentId &&
                enitity.StartDate < item.EndDate && item.StartDate < enitity.EndDate && enitity.Id == default || item.Id != enitity.Id);

            return conflicts.Any()
                ? Result.Failure(RentalErrors.RentalPeriodNotAvaliable)
                : Result.Success();

        }

        private async Task<Result> ValidateUser(Guid userId)
        {
            var userResult = await _usersMicroserviceClient.GetUserByUserId(userId);
            return userResult.IsFailure
                ? Result.Failure(userResult.Error)
                : Result.Success();
        }

    }
}
