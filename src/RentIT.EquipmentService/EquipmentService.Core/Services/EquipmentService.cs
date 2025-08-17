using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.ServiceContracts;

namespace EquipmentService.Core.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ICategoryRepository _categoryRepository;
        public EquipmentService(IEquipmentRepository equipmentRepository, ICategoryRepository categoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> DeleteEquipment(Guid equipmentId)
        {
            var deletionResult = await _equipmentRepository.DeleteEquipmentAsync(equipmentId);

            if(!deletionResult)
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            return Result.Success();   
        }

        public async Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetActiveEquipmentByIdAsync(equipmentId);

            if (equipment == null)
                return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

            return equipment.ToEquipmentResponse();
        }

        public async Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems()
        {
            var equipmentItems = await _equipmentRepository.GetAllActiveEquipmentItemsAsync();
            return equipmentItems.Select(item => item.ToEquipmentResponse());
        }

        public async Task<Result> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request)
        {
            Equipment equipment = request.ToEquipment();

            var validationResult = await ValidateUpdateEquipmentEntity(equipment, equipmentId);
            if (validationResult.IsFailure)
                return Result.Failure<EquipmentResponse>(validationResult.Error);

            bool updationResult = await _equipmentRepository.UpdateEquipmentAsync(equipmentId, equipment);

            if (!updationResult)
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            return Result.Success();
        }

        public async Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request)
        {
            Equipment equipment = request.ToEquipment();
            
            var validationResult = await ValidateNewEquipmentEntity(equipment);
            if (validationResult.IsFailure)
                return Result.Failure<EquipmentResponse>(validationResult.Error);

            var newEquipment = await _equipmentRepository.AddEquipmentAsync(equipment);

            return newEquipment.ToEquipmentResponse();
        }

        private async Task<Result> ValidateNewEquipmentEntity(Equipment equipment)
        {
            if (!await _categoryRepository.DoesCategoryExist(equipment.CategoryId))
                return Result.Failure(CategoryErrors.CategoryNotFound);

            bool isValid = await _equipmentRepository.IsEquipmentUnique(equipment);

            if (!isValid)
                return Result.Failure(EquipmentErrors.EquipmentAlreadyExist);

            return Result.Success();
        }

        private async Task<Result> ValidateUpdateEquipmentEntity(Equipment equipment, Guid equipmentId)
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
