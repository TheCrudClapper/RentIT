using RentalService.Core.Domain.Entities;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace EquipmentService.Core.Validators.ValidatorContracts;

public interface IEntityValidator
{
    Task<Result> ValidateEntity(Rental entity, EquipmentResponse equipmentResponse, bool isUpdate = false);
}

