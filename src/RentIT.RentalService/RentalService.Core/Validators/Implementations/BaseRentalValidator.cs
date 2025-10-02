using EquipmentService.Core.Validators.ValidatorContracts;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.Validators.Implementations;

public abstract class BaseRentalValidator : IEntityValidator
{
    protected readonly IUsersMicroserviceClient _usersMicroserviceClient;
    protected readonly IRentalRepository _rentalRepository;
    public BaseRentalValidator(IUsersMicroserviceClient usersMicroserviceClient,
        IRentalRepository rentalRepository,
        IEquipmentMicroserviceClient equipmentMicroserviceClient)
    {
        _usersMicroserviceClient = usersMicroserviceClient;
        _rentalRepository = rentalRepository;
    }

    public abstract Task<Result> ValidateEntity(Rental entity, EquipmentResponse equipmentResponse, CancellationToken cancellationToken);

    protected virtual async Task<Result> ValidateRentalPeriod(Rental enitity, CancellationToken cancellationToken)
    {
        var conflicts = await _rentalRepository.GetRentalsByCondition(item => item.EquipmentId == enitity.EquipmentId &&
            enitity.StartDate < item.EndDate && item.StartDate < enitity.EndDate && (enitity.Id == default || item.Id != enitity.Id), cancellationToken);

        return conflicts.Any()
            ? Result.Failure(RentalErrors.RentalPeriodNotAvaliable)
            : Result.Success();

    }

    protected virtual async Task<Result> ValidateUser(Guid userId, CancellationToken cancellationToken)
    {
        var userResult = await _usersMicroserviceClient.GetUserByUserId(userId, cancellationToken);
        return userResult.IsFailure
            ? Result.Failure(userResult.Error)
            : Result.Success();
    }
}
