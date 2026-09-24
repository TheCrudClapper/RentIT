using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Entities.Listing.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ServiceContracts;
using EquipmentService.Core.Validators.Contracts;

namespace EquipmentService.Core.Services.ListingServices;

public class UserRentalListingService : IUserRentalListingService
{
    private readonly IRentalListingRepository _rentalListingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRentalListingValidator _validator;

    public UserRentalListingService(
        IRentalListingRepository rentalListingRepository,
        IUnitOfWork unitOfWork,
        IRentalListingValidator validator)
    {
        _rentalListingRepository = rentalListingRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<CreatedResponse>> AddUserRentalListing(Guid userId, RentalListingAddRequest request)
    {
        RentalListing entity = request.ToRentalListing();
        entity.UserId = userId;

        var validation = await _validator.ValidateCreateAsync(entity);

        await _rentalListingRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateUserRentalListing(Guid rentalListingId, Guid userId, RentalListingUpdateRequest request)
    {
        RentalListing? entity = await _rentalListingRepository.GetByIdAsync(rentalListingId);
        if (entity is null)
            return Result.Failure<UpdatedResponse>(RentalListingErrors.RentalListingNotFound);

        RentalListing listing = request.ToRentalListing();
        var validation = await _validator.ValidateUpdateAsync(listing);

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Quantity = request.Quantity;
        entity.PricePerDay = request.PricePerDay.ToCurrency();
        entity.PenaltyFeePerDay = request.PenaltyFeePerDay.ToCurrency();
        entity.MaximumRentalDays = request.MaximumRentalDays;
        entity.MinimumRentalDays = request.MinimumRentalDays;
        entity.DateEdited = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return entity.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<RentalListingListResponse>>> GetAllUserRentalListings(Guid userId, CancellationToken cancellationToken = default)
    {
        var allListings = await _rentalListingRepository.GetAllAsync(cancellationToken);

        var userListings = allListings
            .Select(item => item.ToRentalListingListResponse())
            .ToList();

        return userListings;
    }

    public async Task<Result<RentalListingResponse>> GetUserRentalListingById(Guid rentalListingId, Guid userId,  CancellationToken cancellationToken = default)
    {
        RentalListing? entity = await _rentalListingRepository.GetByConditionAsync(rentalListingId, expression: x => x.UserId == userId, asNoTracking: true, ct: cancellationToken);

        if (entity is null)
            return Result.Failure<RentalListingResponse>(RentalListingErrors.RentalListingNotFound);

        return entity.ToRentalListingResponse();
    }

    public async Task<Result> DeleteUserRentalListing(Guid rentalListingId, Guid userId)
    {
        RentalListing? entity = await _rentalListingRepository.GetByConditionAsync(rentalListingId, x => x.UserId == userId);
        if (entity is null)
            return Result.Failure(RentalListingErrors.RentalListingNotFound);

        entity.Deactivate();
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
