using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Validators.Implementations;

public class EquipmentValidator : IEquipmentValidator
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUsersMicroserviceClient _usersClient;
    public EquipmentValidator(IEquipmentRepository equipmentRepository,
        ICategoryRepository categoryRepository,
        IUsersMicroserviceClient usersClient
        )
    {
        _equipmentRepository = equipmentRepository;
        _categoryRepository = categoryRepository;
        _usersClient = usersClient;
    }

    public async Task<Result> ValidateNewEntity(Equipment entity, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.DoesCategoryExist(entity.CategoryId, cancellationToken))
            return Result.Failure(CategoryErrors.CategoryNotFound);

        var response = await _usersClient.GetUserByUserId(entity.CreatedByUserId, cancellationToken);

        if (response.IsFailure)
            return Result.Failure(response.Error);

        bool isValid = await _equipmentRepository.IsEquipmentUnique(entity, cancellationToken);

        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }

    public async Task<Result> ValidateUpdateEntity(Equipment entity, Guid entityId, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.DoesCategoryExist(entity.CategoryId, cancellationToken))
            return Result.Failure(CategoryErrors.CategoryNotFound);

        var response = await _usersClient.GetUserByUserId(entity.CreatedByUserId, cancellationToken);

        if (response.IsFailure)
            return Result.Failure(response.Error);

        bool isValid = await _equipmentRepository.IsEquipmentUnique(entity, cancellationToken, entityId);

        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }
}
