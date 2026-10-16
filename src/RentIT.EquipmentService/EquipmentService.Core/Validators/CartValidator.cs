using EquipmentService.Core.Domain.Entities.Carts;
using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Validators;

public interface ICartValidator : IEntityValidator<Cart> { }
public class CartValidator : ICartValidator
{
    public Task<Result> ValidateCreateAsync(Cart entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ValidateUpdateAsync(Cart entity)
    {
        throw new NotImplementedException();
    }
}
