using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Entities.Listing.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Validators.Contracts;

namespace EquipmentService.Core.Validators.Implementations;

public class RentalListingValidator : IRentalListingValidator
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IRentalListingRepository _listingRepository;
    public RentalListingValidator(IEquipmentRepository equipmentRepository, IRentalListingRepository listingRepository)
    {
        _equipmentRepository = equipmentRepository;
        _listingRepository = listingRepository;
    }

    public async Task<Result> ValidateCreateAsync(RentalListing entity)
    {
        var baseValidation = await ValidateCommon(entity);
        if(baseValidation.IsFailure)
            return baseValidation;

        if (!await _listingRepository.IsRentalListingUnique(entity))
            return Result.Failure<CreatedResponse>(RentalListingErrors.RentalListingAlreadyExists);

        return Result.Success();

    }

    public async Task<Result> ValidateUpdateAsync(RentalListing entity)
    {
        var baseValidation = await ValidateCommon(entity);
        if (baseValidation.IsFailure)
            return baseValidation;

        if (!await _listingRepository.IsRentalListingUnique(entity, entity.Id))
            return Result.Failure<CreatedResponse>(RentalListingErrors.RentalListingAlreadyExists);

        return Result.Success();
    }

    private async Task<Result> ValidateCommon(RentalListing entity)
    {
        //if eq belongs to user
        if (!await _equipmentRepository.ExistsAsyncByCondition(x => x.UserId == entity.UserId && x.Id == entity.EquipmentId))
            return Result.Failure<CreatedResponse>(RentalListingErrors.RentalListingNotFound);

        if (entity.MinimumRentalDays > entity.MaximumRentalDays)
            return Result.Failure(RentalListingErrors.InvalidRentalDays);

        if (entity.Quantity <= 0)
            return Result.Failure<CreatedResponse>(RentalListingErrors.InvalidQuantity);

        if (entity.PricePerDay.Amount <= 0 || entity.PenaltyFeePerDay.Amount <= 0)
            return Result.Failure<CreatedResponse>(RentalListingErrors.InvalidPrice);

        return Result.Success();
    }
}
