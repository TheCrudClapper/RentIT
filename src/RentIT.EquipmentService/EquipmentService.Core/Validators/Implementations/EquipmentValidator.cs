using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Validators.Implementations;

public class EquipmentValidator : BaseEquipmentValidator, IEquipmentValidator
{
    private readonly IEquipmentRepository _equipmentRepository;
    public EquipmentValidator(IEquipmentRepository equipmentRepository,
        ICategoryRepository categoryRepository,
        IUsersMicroserviceClient usersMicroserviceClient) :base(categoryRepository, usersMicroserviceClient)
    {
        _equipmentRepository = equipmentRepository;
    }

    public override async Task<Result> ValidateEntity(Equipment entity, Guid? entityId = null, CancellationToken cancellationToken = default)
    {
        var categoryValidationResult = await ValidateCategory(entity.CategoryId, cancellationToken);
        if (categoryValidationResult.IsFailure)
            return Result.Failure(categoryValidationResult.Error);

        var userValidationResult = await ValidateUser(entity.CreatedByUserId, cancellationToken);
        if(userValidationResult.IsFailure)
            return Result.Failure(userValidationResult.Error);

        bool isValid = await _equipmentRepository.IsEquipmentUnique(entity, cancellationToken, entityId);

        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }

}
