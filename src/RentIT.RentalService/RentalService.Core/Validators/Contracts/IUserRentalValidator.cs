using EquipmentService.Core.Validators.ValidatorContracts;
using RentalService.Core.Domain.Entities;

namespace RentalService.Core.Validators.Contracts
{
    public interface IUserRentalValidator : IEntityValidator<Rental>
    {
    }
}
