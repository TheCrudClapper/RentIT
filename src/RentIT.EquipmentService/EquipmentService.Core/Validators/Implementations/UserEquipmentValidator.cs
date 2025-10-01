using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Validators.Implementations;

public class UserEquipmentValidator : IUserEquipmentValidator
{
    private readonly IUserEquipmentRepository _userEquipmentRepository;
    private readonly ICategoryRepository _categoryRepository;
    public UserEquipmentValidator(IUserEquipmentRepository userEquipmentRepository, ICategoryRepository categoryRepository)
    {
        _userEquipmentRepository = userEquipmentRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> ValidateNewEntity(Equipment entity, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.DoesCategoryExist(entity.CategoryId, cancellationToken))
            return Result.Failure(CategoryErrors.CategoryNotFound);

        bool isValid = await _userEquipmentRepository.IsEquipmentUnique(entity, cancellationToken);

        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }

    public async Task<Result> ValidateUpdateEntity(Equipment equipment, Guid equipmentId, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.DoesCategoryExist(equipment.CategoryId, cancellationToken))
            return Result.Failure(CategoryErrors.CategoryNotFound);

        bool isValid = await _userEquipmentRepository.IsEquipmentUnique(equipment, cancellationToken, equipmentId);

        if (!isValid)
            return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

        return Result.Success();
    }
}
