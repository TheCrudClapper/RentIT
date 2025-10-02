using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Validators.Implementations;

public class RentalValidator : BaseRentalValidator, IRentalValidator
{
    public RentalValidator(IUsersMicroserviceClient usersMicroserviceClient,IRentalRepository rentalRepository,
        IEquipmentMicroserviceClient equipmentMicroserviceClient) 
        : base(usersMicroserviceClient, rentalRepository, equipmentMicroserviceClient) { }

    public async override Task<Result> ValidateEntity(Rental entity, EquipmentResponse equipmentResponse, CancellationToken cancellationToken)
    {
        var userValidation = await ValidateUser(entity.UserId, cancellationToken);
        if (userValidation.IsFailure)
            return userValidation;

        if (equipmentResponse is not null && entity.UserId == equipmentResponse.CreatedByUserId)
            return Result.Failure(RentalErrors.RentalForSelfEquipment);

        return await ValidateRentalPeriod(entity, cancellationToken);
    }
}
