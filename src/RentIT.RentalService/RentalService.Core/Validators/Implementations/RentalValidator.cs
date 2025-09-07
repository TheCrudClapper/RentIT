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
        private readonly IEquipmentMicroserviceClient _equipmentMicroserviceClient;
        private readonly IRentalRepository _rentalRepository;
        public RentalValidator(IUsersMicroserviceClient usersMicroserviceClient,
            IEquipmentMicroserviceClient equipmentMicroserviceClient,
            IRentalRepository rentalRepository)
        {
            _usersMicroserviceClient = usersMicroserviceClient;
            _equipmentMicroserviceClient = equipmentMicroserviceClient;
            _rentalRepository = rentalRepository;
        }
        public async Task<Result> ValidateNewEntity(Rental entity)
        {
            var userValidation = await ValidateUser(entity.UserId);
            if (userValidation.IsFailure)
                return userValidation;

            return await ValidateRentalPeriod(entity);

        }

        public async Task<Result> ValidateNewEntity(Rental entity, EquipmentResponse equipmentResponse)
        {
            var userValidation = await ValidateUser(entity.UserId);
            if (userValidation.IsFailure)
                return userValidation;

            if (entity.UserId == equipmentResponse.CreatedByUserId)
                return Result.Failure(RentalErrors.RentalForSelfEquipment);

            return await ValidateRentalPeriod(entity);
        }

        public Task<Result> ValidateUpdateEntity(Rental entity, Guid entityId)
        {
            throw new NotImplementedException();
        }

        //PRIVATE HELPER METHODS
        private async Task<Result> ValidateRentalPeriod(Rental enitity)
        {
            var conflicts = await _rentalRepository.GetRentalsByCondition(item => item.EquipmentId == enitity.EquipmentId &&
                enitity.StartDate < item.EndDate && item.StartDate < enitity.EndDate);

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
