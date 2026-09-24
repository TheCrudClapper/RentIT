using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Entities.Listing.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ServiceContracts;

namespace EquipmentService.Core.Services.ListingServices;

public class RentalListingService : IRentalListingService
{
    private readonly IRentalListingRepository _rentalListingRepository;
    public RentalListingService(IRentalListingRepository rentalListingRepository)
    {
        _rentalListingRepository = rentalListingRepository;
    }

    public async Task<Result<IReadOnlyCollection<RentalListingListResponse>>> GetAllRentalListings(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<RentalListing> listings = await _rentalListingRepository.GetAllAsync(cancellationToken);

        return listings
           .Select(item => item.ToRentalListingListResponse())
           .ToList();
    }

    public async Task<Result<RentalListingResponse>> GetRentalListing(Guid id, CancellationToken cancellationToken = default)
    {
        RentalListing? entity = await _rentalListingRepository.GetByIdAsync(id, asNoTracking: true, ct: cancellationToken);

        return entity is null
            ? Result.Failure<RentalListingResponse>(RentalListingErrors.RentalListingNotFound)
            : entity.ToRentalListingResponse();
    }

    public async Task<Result<IReadOnlyCollection<RentalListingResponse>>> GetRentalListingsByEquipmentId(Guid equipmentId, CancellationToken ct)
    {
        IReadOnlyCollection<RentalListing> listings = await _rentalListingRepository.GetByEquipmentIdAsync(equipmentId, ct);

        return listings
            .Select(item => item.ToRentalListingResponse())
            .ToList();
    }
}


