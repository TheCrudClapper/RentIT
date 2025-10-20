using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
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

public class UserEquipmentService : IUserEquipmentService
{
    private readonly IUserEquipmentRepository _userEquipmentRepository;
    private readonly IUserEquipmentValidator _userEquipmentValidator;
    private readonly IUsersMicroserviceClient _usersClient;
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    private readonly IConfiguration _configuration;
    public UserEquipmentService(
        IUserEquipmentRepository userEquipmentRepository,
        IUserEquipmentValidator userEquipmentValidator,
        IUsersMicroserviceClient usersClient,
        IRabbitMQPublisher rabbitMQPublisher,
        IConfiguration configuration
        )
    {
        _userEquipmentRepository = userEquipmentRepository;
        _userEquipmentValidator = userEquipmentValidator;
        _usersClient = usersClient;
        _rabbitMQPublisher = rabbitMQPublisher;
        _configuration = configuration;
    }

    public async Task<Result<EquipmentResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request, CancellationToken cancellationToken)
    {
        var response = await _usersClient.GetUserByUserId(userId, cancellationToken);

        if (response.IsFailure)
            return Result.Failure<EquipmentResponse>(response.Error);

        Equipment equipment = request.ToEquipment();

        var validationResult = await _userEquipmentValidator.ValidateEntity(equipment, null, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure<EquipmentResponse>(validationResult.Error);

        var createdEntity = await _userEquipmentRepository.AddUserEquipment(equipment, userId, cancellationToken);

        //Send message that equipment is created
        _rabbitMQPublisher.Publish<EquipmentResponse>(
            "equipment.create",
            createdEntity.ToEquipmentResponse(),
            _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return createdEntity.ToEquipmentResponse();
    }

    public async Task<Result> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
    {
        var equipmentToUpdate = request.ToEquipment();
        equipmentToUpdate.CreatedByUserId = userId;

        var validationResult = await _userEquipmentValidator.ValidateEntity(equipmentToUpdate, equipmentId, cancellationToken);

        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        var updatedEntity = await _userEquipmentRepository.UpdateUserEquipmentAsync(equipmentId, equipmentToUpdate, cancellationToken);
        if(updatedEntity == null)
            return Result.Failure(EquipmentErrors.EquipmentNotFound);

        _rabbitMQPublisher.Publish(
           "equipment.update",
           updatedEntity.ToEquipmentResponse(),
           _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!);

        return Result.Success();            
    }

    public async Task<Result<EquipmentResponse>> GetUserEquipmentById(Guid userId, Guid equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _userEquipmentRepository.GetUserEquipmentByIdAsync(equipmentId, userId, cancellationToken);

        if (equipment == null)
            return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

        return equipment.ToEquipmentResponse();
    }

    public async Task<IEnumerable<EquipmentResponse>> GetAllUserEquipment(Guid userId, CancellationToken cancellationToken)
    {
        var userEquipments = await _userEquipmentRepository.GetAllUserEquipmentAsync(userId, cancellationToken);
        return userEquipments.Select(item => item.ToEquipmentResponse()).ToList();
    }

    public async Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await _userEquipmentRepository.GetUserEquipmentByIdAsync(equipmentId, userId, cancellationToken);
        if (equipment == null)
            return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

        await _userEquipmentRepository.DeleteUserEquipmentAsync(equipment, cancellationToken);

        //Publish delete message to exchange
        _rabbitMQPublisher.Publish("equipment.delete",
            new EquipmentDeletedMessage(equipmentId),
            _configuration["equipment.exchange"]!);

        return Result.Success();
    }
}
