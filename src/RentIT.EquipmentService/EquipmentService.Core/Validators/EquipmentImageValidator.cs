using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Validators;

public interface IEquipmentImageValidator : IEntityValidator<EquipmentImage> { }

public class EquipmentImageValidator : IEquipmentImageValidator
{
    public Task<Result> ValidateCreateAsync(EquipmentImage entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ValidateUpdateAsync(EquipmentImage entity)
    {
        throw new NotImplementedException();
    }
}

