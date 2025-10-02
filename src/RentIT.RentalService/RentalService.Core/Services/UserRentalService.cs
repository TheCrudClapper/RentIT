using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Mappings;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Services;

public class UserRentalService : IUserRentalService
{
    private readonly IUserRentalRepository _userRentalRepository;
    private readonly IUserRentalValidator _userRentalValidator;
    private readonly IEquipmentMicroserviceClient _equipmentMicroserviceClient;
    public UserRentalService(IUserRentalRepository userRentalRepository,
        IUserRentalValidator validator,
        IEquipmentMicroserviceClient equipmentMicroserviceClient)
    {
        _userRentalRepository = userRentalRepository;
        _userRentalValidator = validator;
        _equipmentMicroserviceClient = equipmentMicroserviceClient;
    }
    public async Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var rental = request.ToRental(userId);

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        var validationResult = await _userRentalValidator.ValidateEntity(rental, equipmentResponse.Value, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure<RentalResponse>(validationResult.Error);

        rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
            rental.EndDate,
            equipmentResponse.Value.RentalPricePerDay);

        Rental newRental = await _userRentalRepository.AddRentalAsync(rental, userId, cancellationToken);
        return newRental.ToRentalResponse(equipmentResponse.Value);

    }

    public async Task<Result> DeleteRental(Guid rentalId, Guid userId, CancellationToken cancellationToken)
    {
        bool isSuccess = await _userRentalRepository.DeleteRentalAsync(rentalId, userId, cancellationToken);

        if (!isSuccess)
            return Result.Failure(RentalErrors.RentalNotFound);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(Guid userId, CancellationToken cancellationToken)
    {
        var rentals = await _userRentalRepository.GetAllRentalsAsync(userId, cancellationToken);

        var equipmentIds = rentals.Select(x => x.EquipmentId).Distinct();

        var eqResponse = await _equipmentMicroserviceClient.GetEquipmentsByIds(equipmentIds, cancellationToken);
        if (eqResponse.IsFailure)
            return Result.Failure<IEnumerable<RentalResponse>>(eqResponse.Error);

        var equipmentDict = eqResponse.Value.ToDictionary(x => x.Id, x => x);

        return rentals
        .Where(r => equipmentDict.ContainsKey(r.EquipmentId))
        .Select(r => r.ToRentalResponse(equipmentDict[r.EquipmentId]))
        .ToList();
    }

    public async Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId, CancellationToken cancellationToken)
    {
        Rental? rental = await _userRentalRepository.GetRentalByIdAsync(rentalId, userId, cancellationToken);
        if (rental == null)
            return Result.Failure<RentalResponse>(RentalErrors.RentalNotFound);

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        return rental.ToRentalResponse(equipmentResponse.Value);
    }

    public async Task<Result> UpdateRental(Guid rentalId, UserRentalUpdateRequest request, Guid userId, CancellationToken cancellationToken)
    {
        Rental rental = request.ToRentalEntity();

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        var validationResult = await _userRentalValidator.ValidateEntity(rental, equipmentResponse.Value, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
            rental.EndDate,
            equipmentResponse.Value.RentalPricePerDay);

        bool isSuccess = await _userRentalRepository.UpdateRentalAsync(rentalId, rental, userId, cancellationToken);

        if (!isSuccess)
            return Result.Failure(RentalErrors.RentalNotFound);

        return Result.Success();
    }
    private decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, decimal dailyPrice)
    {
        var days = (endDate.Date - startDate.Date).Days;
        return dailyPrice * days;
    }
}
