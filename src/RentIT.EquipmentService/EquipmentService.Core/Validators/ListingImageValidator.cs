using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Validators;

public interface IListingImageValidator : IEntityValidator<ListingImage> { }
internal class ListingImageValidator : IListingImageValidator
{
    public Task<Result> ValidateCreateAsync(ListingImage entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ValidateUpdateAsync(ListingImage entity)
    {
        throw new NotImplementedException();
    }
}
