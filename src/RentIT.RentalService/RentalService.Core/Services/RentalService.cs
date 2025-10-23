using Microsoft.Extensions.Configuration;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.HtppClientContracts;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Mappings;
using RentalService.Core.RabbitMQ.Messages;
using RentalService.Core.RabbitMQ.Publishers;
using RentalService.Core.ResultTypes;
using RentalService.Core.ServiceContracts;
using RentalService.Core.Validators.Contracts;

namespace RentalService.Core.Services;

public class RentalService :BaseRentalService, IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IRentalValidator _rentalValidator;
    private readonly IEquipmentMicroserviceClient _equipmentMicroserviceClient;
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    public RentalService(IRentalRepository rentalRepository,
        IRentalValidator rentalValidator,
        IEquipmentMicroserviceClient equipmentMicroserviceClient,
        IConfiguration configuration,
        IRabbitMQPublisher rabbitMQPublisher) :base(configuration)
    {
        _rentalRepository = rentalRepository;
        _rentalValidator = rentalValidator;
        _equipmentMicroserviceClient = equipmentMicroserviceClient;
        _rabbitMQPublisher = rabbitMQPublisher;
    }

    public async Task<Result<RentalResponse>> AddRental(RentalAddRequest request, CancellationToken cancellationToken)
    {
        Rental rental = request.ToRentalEntity();

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        var validationResult = await _rentalValidator.ValidateEntity(rental, equipmentResponse.Value, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure<RentalResponse>(validationResult.Error);

        //Calculating Total Rental Price
        rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
            rental.EndDate,
            equipmentResponse.Value.RentalPricePerDay);

        Rental newRental = await _rentalRepository.AddRentalAsync(rental, cancellationToken);            
        return newRental.ToRentalResponse(equipmentResponse.Value);
    }

    public async Task<Result> DeleteRental(Guid rentalId, CancellationToken cancellationToken)
    {
        bool isSuccess = await _rentalRepository.DeleteRentalAsync(rentalId, cancellationToken);

        if (!isSuccess)
            return Result.Failure(RentalErrors.RentalNotFound);

        return Result.Success();
    }

    public async Task<Result<RentalResponse>> GetRental(Guid rentalId, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.GetRentalByIdAsync(rentalId, cancellationToken);
        if (rental == null)
            return Result.Failure<RentalResponse>(RentalErrors.RentalNotFound);

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        return rental.ToRentalResponse(equipmentResponse.Value);
    }

    public async Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
    {
        var rentals = await _rentalRepository.GetAllRentalsAsync(cancellationToken);

        var equipmentIds = rentals
            .Select(x => x.EquipmentId)
            .Distinct()
            .ToList();
        
        if (equipmentIds.Count == 0)
            return Result.Success(Enumerable.Empty<RentalResponse>());

        var eqResponse = await _equipmentMicroserviceClient.GetEquipmentsByIds(equipmentIds, cancellationToken);
        if (eqResponse.IsFailure)
            return Result.Failure<IEnumerable<RentalResponse>>(eqResponse.Error);

        var equipmentDict = eqResponse.Value
            .ToDictionary(x => x.Id, x => x);

        return rentals
        .Where(r => equipmentDict.ContainsKey(r.EquipmentId))
        .Select(r => r.ToRentalResponse(equipmentDict[r.EquipmentId]))
        .ToList();
    }

    public async Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request, CancellationToken cancellationToken)
    {
        Rental rental = request.ToRentalEntity();

        var equipmentResponse = await _equipmentMicroserviceClient.GetEquipment(rental.EquipmentId, cancellationToken);
        if (equipmentResponse.IsFailure)
            return Result.Failure<RentalResponse>(equipmentResponse.Error);

        var validationResult = await _rentalValidator.ValidateEntity(rental, equipmentResponse.Value, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        rental.RentalPrice = CalculateTotalRentalPrice(rental.StartDate,
            rental.EndDate,
            equipmentResponse.Value.RentalPricePerDay);

        bool isSuccess = await _rentalRepository.UpdateRentalAsync(rentalId, rental, cancellationToken);

        if (!isSuccess)
            return Result.Failure(RentalErrors.RentalNotFound);

        return Result.Success();
    }

    public async Task<Result> DeleteRentalByEquipmentId(Guid equipmentId, CancellationToken cancellationToken)
    {
        bool isSuccess = await _rentalRepository.DeleteRentalsByEquipmentAsync(equipmentId, cancellationToken);

        if (!isSuccess)
            return Result.Failure(RentalErrors.FailedToDeleteRelatedRentals);

        return Result.Success();
    }

    public async Task<Result> MarkEquipmentAsReturned(ReturnEquipmentRequest request, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.GetRentalByIdAsync(request.RentalId, cancellationToken);

        if (rental is null)
            return Result.Failure(RentalErrors.RentalNotFound);

        var equipmentResponse = await _equipmentMicroserviceClient
            .GetEquipment(rental.EquipmentId, cancellationToken);

        if (equipmentResponse.IsFailure)
            return Result.Failure(equipmentResponse.Error);

        var dateDiff = request.ReturnedDate.Date - rental.StartDate.Date;
        if (dateDiff.TotalDays < 1)
            return Result.Failure(RentalErrors.InvalidReturnedDate);

        await _rentalRepository.MarkEquipmentAsReturned(rental, request.ReturnedDate, cancellationToken);

        var calculatedTotalValue = CalculateTotalRentalPrice
            (rental.StartDate, rental.EndDate, request.ReturnedDate, equipmentResponse.Value.RentalPricePerDay);

        if (calculatedTotalValue != rental.RentalPrice)
            await _rentalRepository.UpdateRentalTotalCost(rental, calculatedTotalValue, cancellationToken);

        var message = new ReviewAllowanceAddRequest
        {
            EquipmentId = rental.EquipmentId,
            RentalId = rental.Id,
            UserId = rental.UserId,
        };

        _rabbitMQPublisher.Publish(
            "review.allowance.create",
            message,
            _configuration["RABBITMQ_RENTAL_EXCHANGE"]!);
        
        return Result.Success();
    }
}
