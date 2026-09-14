using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Entities.Equipments.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.Equipments;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.RabbitMQ.Messages;
using EquipmentService.Core.RabbitMQ.Publishers;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Validators.Contracts;
using Microsoft.Extensions.Configuration;

namespace EquipmentService.Core.Services.EquipmentServices;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IEquipmentValidator _equipmentValidator;
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public EquipmentService(IEquipmentRepository equipmentRepository,
        IEquipmentValidator equipmentValidator,
        IRabbitMQPublisher rabbitMQ,
        IConfiguration configuration,
        IUnitOfWork unitOfWork)
    {
        _equipmentRepository = equipmentRepository;
        _equipmentValidator = equipmentValidator;
        _rabbitMQPublisher = rabbitMQ;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> DeleteEquipment(Guid equipmentId)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
        if (equipment == null)
            return Result.Failure(EquipmentErrors.EquipmentNotFound);

        equipment.IsActive = false;
        equipment.DateDeleted = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        //Delete corresponding rentals
        _rabbitMQPublisher.Publish("equipment.delete",
            new EquipmentDeletedMessage(equipmentId),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return Result.Success();
    }

    public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken ct)
    {
        Equipment? equipment = await _equipmentRepository.GetByIdAsync(equipmentId, ct: ct);

        return equipment is null
            ? Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound)
            : equipment.ToEquipmentResponse();
    }

    public async Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentItems(CancellationToken ct)
    {
        var equipmentItems = await _equipmentRepository.GetAllAsync(ct);

        return equipmentItems
            .Select(item => item.ToEquipmentResponse())
            .ToList();
    }

    public async Task<Result<UpdatedResponse>> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request)
    {
        Equipment equipment = request.ToEquipment();
        var validationResult = await _equipmentValidator.ValidateEntity(equipment, equipmentId);
        if (validationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(validationResult.Error);

        Equipment? entity = await _equipmentRepository.GetByIdAsync(equipmentId);
        if (entity is null)
            return Result.Failure<UpdatedResponse>(EquipmentErrors.EquipmentNotFound);

        entity.Name = equipment.Name;
        entity.Status = equipment.Status;
        entity.Notes = equipment.Notes;
        entity.CreatedByUserId = equipment.CreatedByUserId;
        entity.RentalPricePerDay = equipment.RentalPricePerDay;
        entity.SerialNumber = equipment.SerialNumber;
        entity.CategoryId = equipment.CategoryId;

        await _unitOfWork.SaveChangesAsync();

        //Publish update message
        _rabbitMQPublisher.Publish(
            "equipment.update",
            entity.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!
            );

        return entity.ToUpdatedResponse();
    }

    public async Task<Result<CreatedResponse>> AddEquipment(EquipmentAddRequest request)
    {
        Equipment equipment = request.ToEquipment();
        var validationResult = await _equipmentValidator.ValidateEntity(equipment, null);
        if (validationResult.IsFailure)
            return Result.Failure<CreatedResponse>(validationResult.Error);

        await _equipmentRepository.AddAsync(equipment);
        
        //Publish create message
        _rabbitMQPublisher.Publish(
            "equipment.create",
            equipment.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!
            );

        return equipment.ToCreatedResponse();
    }

    public async Task<Result<bool>> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken)
    {
        var exists = await _equipmentRepository
            .DoesEquipmentExistsAsync(equipmentId, cancellationToken);

        if (!exists)
            return Result.Failure<bool>(EquipmentErrors.EquipmentNotFound);

        return exists;
    }

    public async Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken)
    {
        var equipmentsByCondition = await _equipmentRepository
            .GetEquipmentsByCondition(item => equipmentIds.Contains(item.Id), cancellationToken);

        return equipmentsByCondition
            .Select(equipment => equipment.ToEquipmentResponse())
            .ToList();
    }

    public async Task UpdateEquipmentRating(Guid equipmentId, decimal rating, decimal? oldRating = null)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

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

    public async Task DeleteEquipmentRating(Guid equipmentId, decimal rating)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

        if (equipment is null) return;


        var oldCount = equipment.ReviewCount;

        if (oldCount <= 0)
            return;

        if (oldCount == 1)
        {
            await _equipmentRepository.UpdateEquipmentRating(equipment, 0, -1);
            return;
        }

        var newAverageRating = ((equipment.AverageRating * oldCount) - rating) / (oldCount - 1);
        await _equipmentRepository.UpdateEquipmentRating(equipment, newAverageRating, -1);
    }

}
