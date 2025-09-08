using EquipmentService.Core.Validators.ValidatorContracts;
using RentalService.Core.Domain.Entities;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.Validators.Contracts
{
    public interface IRentalValidator : IEntityValidator<Rental>
    {
        Task<Result> ValidateNewEntity(Rental entity, EquipmentResponse equipmentResponse);
        Task<Result> ValidateUpdateEntity(Rental entity, Guid rentalId, EquipmentResponse equipmentResponse);
    }
}
