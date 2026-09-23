using EquipmentService.Core.Domain.Entities.Categories.Errors;
using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.Validators.Contracts;

namespace EquipmentService.Core.Validators.Implementations;

public class EquipmentValidator : IEquipmentValidator
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUsersMicroserviceClient _usersClient;

    public EquipmentValidator(ICategoryRepository categoryRepository,
        IUsersMicroserviceClient userClient)
    {
        _usersClient = userClient;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> ValidateCreateAsync(Equipment entity)
        => await ValidateCommon(entity);

    public async Task<Result> ValidateUpdateAsync(Equipment entity)
        => await ValidateCommon(entity);

    private async Task<Result> ValidateCommon(Equipment entity, CancellationToken cancellationToken = default)
    {
        bool categoryExists = await _categoryRepository.ExistsAsync(entity.CategoryId);
        if (!categoryExists)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        var userValidationResult = await _usersClient.GetUserByUserId(entity.UserId, cancellationToken);
        if (userValidationResult.IsFailure)
            return Result.Failure(userValidationResult.Error);

        return Result.Success();
    }
}
