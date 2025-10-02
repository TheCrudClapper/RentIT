using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Validators.Implementations;

public class UserEquipmentValidator : BaseEquipmentValidator, IUserEquipmentValidator
{
    private readonly IUserEquipmentRepository _userEquipmentRepository;
    public UserEquipmentValidator(IUserEquipmentRepository userEquipmentRepository,
        ICategoryRepository categoryRepository,
        IUsersMicroserviceClient usersMicroserviceClient) :base(categoryRepository, usersMicroserviceClient)
    {
        _userEquipmentRepository = userEquipmentRepository;
    }

    public async override Task<Result> ValidateEntity(Equipment entity, Guid? entityId = null, CancellationToken cancellationToken = default)
    {
        var categoryValidationResult = await ValidateCategory(entity.CategoryId, cancellationToken);
        if (categoryValidationResult.IsFailure)
            return Result.Failure(categoryValidationResult.Error);

        var isValid = await _userEquipmentRepository.IsEquipmentUnique(entity, cancellationToken, entityId);
        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }
}
