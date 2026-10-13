using RentalService.Core.Domain.Entities.Errors;
using RentalService.Core.Domain.Entities.Rentals;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.Domain.ResultTypes;
using RentalService.Core.Validators.Contracts;
using RentIT.Contracts.DTO.RentalMicroservice.Rentals;

namespace RentalService.Core.Validators.Implementations;

public class UserRentalValidator : BaseRentalValidator, IUserRentalValidator
{
    public UserRentalValidator(IUsersMicroserviceClient usersMicroserviceClient, IRentalRepository rentalRepository,
       IEquipmentMicroserviceClient equipmentMicroserviceClient)
       : base(usersMicroserviceClient, rentalRepository, equipmentMicroserviceClient) { }

    public async override Task<Result> ValidateEntity(Rental entity, EquipmentResponse equipmentResponse, CancellationToken cancellationToken)
    {
        if (equipmentResponse is not null && entity.UserId == equipmentResponse.CreatedByUserId)
            return Result.Failure(RentalErrors.RentalForSelfEquipment);

        return await ValidateRentalPeriod(entity, cancellationToken);
    }
}
