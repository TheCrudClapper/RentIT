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
        private readonly IUserEquipmentRepository _userEquipmentRepository;
        private readonly IUserEquipmentValidator _userEquipmentValidator;
        public UserEquipmentService(IUserEquipmentRepository userEquipmentRepository, IUserEquipmentValidator userEquipmentValidator)
        {
            _userEquipmentRepository = userEquipmentRepository;
            _userEquipmentValidator = userEquipmentValidator;
        }

        public async Task<Result<EquipmentResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request)
        {
            Equipment equipment = request.ToEquipment();

            var validationResult = await _userEquipmentValidator.ValidateNewEntity(equipment);

            if (validationResult.IsFailure)
                return Result.Failure<EquipmentResponse>(validationResult.Error);

            var newEquipment = await _userEquipmentRepository.AddUserEquipment(equipment, userId);

            return newEquipment.ToEquipmentResponse();
        }

        public async Task<Result> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request)
        {
            var equipment = await _userEquipmentRepository.GetUserEquipmentByIdAsync(equipmentId, userId);

            if (equipment == null) 
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            var equipmentToUpdate = request.ToEquipment();

            var validationResult = await _userEquipmentValidator.ValidateUpdateEntity(equipmentToUpdate, equipmentId);

            if (validationResult.IsFailure)
                return Result.Failure(validationResult.Error);

            await _userEquipmentRepository.UpdateUserEquipmentAsync(equipmentToUpdate);

            return Result.Success();            
        }

        public async Task<Result<EquipmentResponse>> GetUserEquipment(Guid userId, Guid equipmentId)
        {
            var equipment = await _userEquipmentRepository.GetUserEquipmentByIdAsync(userId, equipmentId);

            if (equipment == null)
                return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

            return equipment.ToEquipmentResponse();
        }

        public async Task<IEnumerable<EquipmentResponse>> GetAllUserEquipment(Guid userId)
        {
            var userEquipments = await _userEquipmentRepository.GetAllUserEquipmentAsync(userId);
            return userEquipments.Select(item => item.ToEquipmentResponse());
        }

        public async Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId)
        {
            bool isDeleted = await _userEquipmentRepository.DeleteUserEquipmentAsync(userId, equipmentId);

            if(!isDeleted)
                return Result.Failure(EquipmentErrors.FailedToDeleteEquipment);

            return Result.Success();
        }
    }
}
