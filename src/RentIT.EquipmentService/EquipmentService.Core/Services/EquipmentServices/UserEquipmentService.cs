using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Services.EquipmentServices
{
    public class UserEquipmentService : IUserEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserEquipmentValidator _userEquipmentValidator;
        public UserEquipmentService(IEquipmentRepository equipmentRepository, IUserEquipmentValidator userEquipmentValidator)
        {
            _equipmentRepository = equipmentRepository;
            _userEquipmentValidator = userEquipmentValidator;
        }

        //ok
        public async Task<Result<EquipmentResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request)
        {
            Equipment equipment = request.ToEquipment();

            var validationResult = await _userEquipmentValidator.ValidateNewEntity(equipment);

            var newEquipment = await _equipmentRepository.AddUserEquipment(equipment, userId);

            return newEquipment.ToEquipmentResponse();
        }

        //ok
        public async Task<Result> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request)
        {
            var equipment = await _equipmentRepository.GetUserEquipmentByIdAsync(equipmentId, userId);

            if (equipment == null) 
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            var equipmentToUpdate = request.ToEquipment();

            var validationResult = await _userEquipmentValidator.ValidateUpdateEntity(equipmentToUpdate, equipmentId);

            if (validationResult.IsFailure)
                return Result.Failure(validationResult.Error);

            await _equipmentRepository.UpdateUserEquipmentAsync(equipmentToUpdate);

            return Result.Success();            
        }

        //ok
        public async Task<Result<EquipmentResponse>> GetUserEquipment(Guid userId, Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetUserEquipmentByIdAsync(userId, equipmentId);

            if (equipment == null)
                return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

            return equipment.ToEquipmentResponse();
        }

        //ok
        public async Task<IEnumerable<EquipmentResponse>> GetAllUserEquipment(Guid userId)
        {
            var userEquipments = await _equipmentRepository.GetAllUserEquipmentAsync(userId);
            return userEquipments.Select(item => item.ToEquipmentResponse());
        }

        //ok
        public async Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId)
        {
            bool isDeleted = await _equipmentRepository.DeleteUserEquipmentAsync(userId, equipmentId);

            if(!isDeleted)
                return Result.Failure(EquipmentErrors.FailedToDeleteEquipment);

            return Result.Success();
        }
    }
}
