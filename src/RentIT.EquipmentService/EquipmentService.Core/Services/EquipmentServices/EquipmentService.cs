using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.RabbitMQ.Messages;
using EquipmentService.Core.RabbitMQ.Publishers;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Validators.ValidatorContracts;
using Microsoft.Extensions.Configuration;

namespace EquipmentService.Core.Services.EquipmentServices;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IEquipmentValidator _equipmentValidator;
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    private readonly IConfiguration _configuration;
    public EquipmentService(IEquipmentRepository equipmentRepository,
        IEquipmentValidator equipmentValidator,
        IRabbitMQPublisher rabbitMQ,
        IConfiguration configuration)
    {
        _equipmentRepository = equipmentRepository;
        _equipmentValidator = equipmentValidator;
        _rabbitMQPublisher = rabbitMQ;
        _configuration = configuration;
    }

    public async Task<Result> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId, cancellationToken);
        if (equipment == null)
            return Result.Failure(EquipmentErrors.EquipmentNotFound);

        //Delete EQ
        await _equipmentRepository.DeleteEquipmentAsync(equipment, cancellationToken);

        //Delete corresponding rentals
        _rabbitMQPublisher.Publish("equipment.delete",
            new EquipmentDeletedMessage(equipmentId),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return Result.Success();
    }

    public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId, cancellationToken);

        if (equipment == null)
            return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

        return equipment.ToEquipmentResponse();
    }

    public async Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems(CancellationToken cancellationToken)
    {
        var equipmentItems = await _equipmentRepository.GetAllEquipmentAsync(cancellationToken);

        return equipmentItems
            .Select(item => item.ToEquipmentResponse())
            .ToList();
    }

    public async Task<Result> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
    {
        Equipment equipment = request.ToEquipment();

        var validationResult = await _equipmentValidator.ValidateEntity(equipment, equipmentId, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<EquipmentResponse>(validationResult.Error);

        var updatedEntity = await _equipmentRepository.UpdateEquipmentAsync(equipmentId, equipment, cancellationToken);

        if (updatedEntity == null)
            return Result.Failure(EquipmentErrors.EquipmentNotFound);

        //Publish update message
        _rabbitMQPublisher.Publish(
            "equipment.update",
            updatedEntity.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!
            );

        return Result.Success();
    }

    public async Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request, CancellationToken cancellationToken)
    {
        Equipment equipment = request.ToEquipment();

        var validationResult = await _equipmentValidator.ValidateEntity(equipment, null, cancellationToken);
        if (validationResult.IsFailure)
            return Result.Failure<EquipmentResponse>(validationResult.Error);

        var createdEntity = await _equipmentRepository.AddEquipmentAsync(equipment, cancellationToken);

        //Publish create message
        _rabbitMQPublisher.Publish(
            "equipment.create",
            createdEntity.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!
            );

        return createdEntity.ToEquipmentResponse();
    }

    public async Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken)
    {
        var exists = await _equipmentRepository
            .DoesEquipmentExistsAsync(equipmentId, cancellationToken);

        if (!exists)
            return Result.Failure<bool>(EquipmentErrors.EquipmentNotFound);

        return exists;
    }

    public async Task<IEnumerable<EquipmentResponse>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken)
    {
        var equipmentsByCondition = await _equipmentRepository
            .GetEquipmentsByCondition(item => equipmentIds.Contains(item.Id), cancellationToken);

        return equipmentsByCondition
            .Select(equipment => equipment.ToEquipmentResponse())
            .ToList();
    }

    public async Task UpdateEquipmentRating(Guid equipmentId, decimal rating, decimal? oldRating = null, CancellationToken cancellationToken = default)
    {
       var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId, cancellationToken);

        if (equipment is null) return;

        decimal newAverageRating;

        if (oldRating is null)
        {
            var oldReviewCount = equipment.ReviewCount;
            newAverageRating = ((equipment.AverageRating * oldReviewCount) + rating) / (oldReviewCount + 1);
            await _equipmentRepository.UpdateEquipmentRating(equipment, newAverageRating, 1);
        }
        else
        {
            newAverageRating = ((equipment.AverageRating * equipment.ReviewCount) - oldRating.Value + rating)
             / equipment.ReviewCount;
            await _equipmentRepository.UpdateEquipmentRating(equipment, newAverageRating);
        }

    }

    public async Task DeleteEquipmentRating(Guid equipmentId, decimal rating, CancellationToken cancellationToken = default)
    {
        var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId, cancellationToken);

        if (equipment is null) return;

        decimal newAverageRating;

        //guard not to divide by zero
        var oldReviewCount = equipment.ReviewCount;
        //substract value, and evaluate new average
        newAverageRating = ((equipment.AverageRating * oldReviewCount) - rating) / (oldReviewCount - 1);

        await _equipmentRepository.UpdateEquipmentRating(equipment, newAverageRating, -1);
    }
}
