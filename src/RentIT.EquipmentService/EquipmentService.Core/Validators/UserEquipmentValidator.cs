using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.Validators
{
    public class UserEquipmentValidator
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UserEquipmentValidator(IEquipmentRepository equipmentRepository, ICategoryRepository categoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> ValidateAddEntity(Equipment equipment)
        {
            if (!await _categoryRepository.DoesCategoryExist(equipment.CategoryId))
                return Result.Failure(CategoryErrors.CategoryNotFound);

            bool isValid = await _equipmentRepository.IsEquipmentUnique(equipment);

            if (!isValid)
                return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

            return Result.Success();
        }

        public async Task<Result> ValidateUpdateEntity(Equipment equipment, Guid equipmentId)
        {
            if (!await _categoryRepository.DoesCategoryExist(equipment.CategoryId))
                return Result.Failure(CategoryErrors.CategoryNotFound);

            bool isValid = await _equipmentRepository.IsEquipmentUnique(equipment, equipmentId);

            if (!isValid)
                return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

            return Result.Success();
        }
    }
}
