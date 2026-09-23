using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Entities.Equipments.Errors;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.Equipments.Admin;
using EquipmentService.Core.DTO.Equipments.User;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.RabbitMQ.Messages;
using EquipmentService.Core.RabbitMQ.Publishers;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Validators.Contracts;
using Microsoft.Extensions.Configuration;

namespace EquipmentService.Core.Services.EquipmentServices;

public class UserEquipmentService : IUserEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IEquipmentValidator _equipmentValidator;
    private readonly IRabbitMQPublisher _rabbitMqPublisher;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public UserEquipmentService(
        IEquipmentRepository equipmentRepository,
        IEquipmentValidator equipmentValidator,
        IUsersMicroserviceClient usersClient,
        IRabbitMQPublisher rabbitMqPublisher,
        IConfiguration configuration,
        IUnitOfWork uow)
    {
        _equipmentRepository = equipmentRepository;
        _equipmentValidator = equipmentValidator;
        _rabbitMqPublisher = rabbitMqPublisher;
        _configuration = configuration;
        _unitOfWork = uow;
    }

    public async Task<Result<CreatedResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request)
    {
        Equipment equipment = request.ToUserEquipment();
        equipment.UserId = userId;

        var validationResult = await _equipmentValidator.ValidateCreateAsync(equipment);

        if (validationResult.IsFailure)
            return Result.Failure<CreatedResponse>(validationResult.Error);

        await _equipmentRepository.AddAsync(equipment);
        await _unitOfWork.SaveChangesAsync();

        _rabbitMqPublisher.Publish(
            "equipment.create",
            equipment.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return equipment.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request)
    {
        Equipment? entity = await _equipmentRepository.GetByConditionAsync(equipmentId, x => x.UserId == userId);

        if (entity is null)
            return Result.Failure<UpdatedResponse>(EquipmentErrors.EquipmentNotFound);

        Equipment equipmentToUpdate = request.ToEquipment();

        var validationResult = await _equipmentValidator.ValidateUpdateAsync(equipmentToUpdate);

        if (validationResult.IsFailure)
            return Result.Failure<UpdatedResponse>(validationResult.Error);

        entity.Update(equipmentToUpdate);
        await _unitOfWork.SaveChangesAsync();

        _rabbitMqPublisher.Publish(
           "equipment.update",
           entity.ToEquipmentResponse(),
           _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return entity.ToUpdatedResponse();
    }

    public async Task<Result<EquipmentResponse>> GetUserEquipmentById(Guid userId, Guid equipmentId, CancellationToken ct)
    {
        Equipment? equipment = await _equipmentRepository.GetByConditionAsync(equipmentId, expression: x => x.UserId == userId, asNoTracking: true, ct: ct);

        if (equipment is null)
            return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

        return equipment.ToEquipmentResponse();
    }

    public async Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetAllUserEquipment(Guid userId, CancellationToken ct)
    {
        var userEquipments = await _equipmentRepository.GetAllAsyncByCondition(
            e => e.UserId == userId,
            ct);

        return userEquipments
            .Select(item => item.ToUserEquipmentListResponse())
            .ToList();
    }

    public async Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId)
    {
        Equipment? equipment = await _equipmentRepository.GetByConditionAsync(equipmentId, x => x.UserId == userId);

        if (equipment is null)
            return Result.Failure(EquipmentErrors.EquipmentNotFound);

        equipment.Deactivate();
        await _unitOfWork.SaveChangesAsync();

        _rabbitMqPublisher.Publish("equipment.delete",
            new EquipmentDeletedMessage(equipmentId),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return Result.Success();
    }
}
