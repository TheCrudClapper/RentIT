using RentalService.Core.Domain.Entities.Rentals;
using RentalService.Core.Domain.ResultTypes;
using RentIT.Contracts.DTO.RentalMicroservice.Rentals;

namespace RentalService.Core.Validators.Contracts;

public interface IEntityValidator
{
    Task<Result> ValidateEntity(Rental entity,
        EquipmentResponse equipmentResponse,
        CancellationToken cancellationToken);
}

