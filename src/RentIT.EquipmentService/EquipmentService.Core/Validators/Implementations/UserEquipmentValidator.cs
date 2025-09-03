using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Validators.Implementations
{
    public class UserEquipmentValidator : IUserEquipmentValidator
    {
        private readonly IUserEquipmentRepository _userEquipmentRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UserEquipmentValidator(IUserEquipmentRepository userEquipmentRepository, ICategoryRepository categoryRepository)
        {
            _userEquipmentRepository = userEquipmentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> ValidateNewEntity(Equipment entity)
        {
            if (!await _categoryRepository.DoesCategoryExist(entity.CategoryId))
                return Result.Failure(CategoryErrors.CategoryNotFound);

            bool isValid = await _userEquipmentRepository.IsEquipmentUnique(entity);

            if (!isValid)
                return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

            return Result.Success();
        }

        public async Task<Result> ValidateUpdateEntity(Equipment equipment, Guid equipmentId)
        {
            if (!await _categoryRepository.DoesCategoryExist(equipment.CategoryId))
                return Result.Failure(CategoryErrors.CategoryNotFound);

            bool isValid = await _userEquipmentRepository.IsEquipmentUnique(equipment, equipmentId);

            if (!isValid)
                return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

            return Result.Success();
        }
    }
}
