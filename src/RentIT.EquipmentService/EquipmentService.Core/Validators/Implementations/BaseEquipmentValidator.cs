using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.Validators.Implementations;

public abstract class BaseEquipmentValidator
{
    protected readonly ICategoryRepository _categoryRepository;
    protected readonly IUsersMicroserviceClient _usersMicroserviceClient;

    public BaseEquipmentValidator(
        ICategoryRepository categoryRepository,
        IUsersMicroserviceClient usersMicroserviceClient)
    {
        _categoryRepository = categoryRepository;
        _usersMicroserviceClient = usersMicroserviceClient;
    }

    public abstract Task<Result> ValidateEntity(Equipment entity, Guid? entityId = null, CancellationToken cancellationToken = default);
    public async Task<Result> ValidateUser(Guid userId, CancellationToken cancellationToken)
    {
        var userResult = await _usersMicroserviceClient.GetUserByUserId(userId, cancellationToken);
        return userResult.IsFailure
            ? Result.Failure(UserErrors.UserNotFound)
            : Result.Success();
    }

    public async Task<Result> ValidateCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var categoryResult = await _categoryRepository
            .DoesCategoryExist(categoryId, cancellationToken);

        return categoryResult
            ? Result.Success()
            : Result.Failure(CategoryErrors.CategoryNotFound);
    }
}
