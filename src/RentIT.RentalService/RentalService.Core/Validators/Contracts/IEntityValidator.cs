using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.ResultTypes;
using RentalService.Core.DTO.Rentals;

namespace EquipmentService.Core.Validators.ValidatorContracts;

public interface IEntityValidator
{
    Task<Result> ValidateEntity(Rental entity,
        EquipmentResponse equipmentResponse,
        CancellationToken cancellationToken);
}

