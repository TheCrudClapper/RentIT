using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.ServiceContracts.Equipment;
using EquipmentService.Core.Validators.ValidatorContracts;

namespace EquipmentService.Core.Services.EquipmentServices
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentValidator _equipmentValidator;
        private readonly IUsersMicroserviceClient _usersMicroserviceClient;
        public EquipmentService(IEquipmentRepository equipmentRepository,
            IEquipmentValidator equipmentValidator,
            IUsersMicroserviceClient usersMicroserviceClient)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentValidator = equipmentValidator;
            _usersMicroserviceClient = usersMicroserviceClient;
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
            var equipment = await _equipmentRepository.GetEquipmentByIdAsync(equipmentId);

            if (equipment == null)
                return Result.Failure<EquipmentResponse>(EquipmentErrors.EquipmentNotFound);

            return equipment.ToEquipmentResponse();
        }

        public async Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems()
        {
            var equipmentItems = await _equipmentRepository.GetAllEquipmentAsync();

            return equipmentItems.Select(item => item.ToEquipmentResponse());
        }

        public async Task<Result> UpdateEquipment(Guid equipmentId, EquipmentUpdateRequest request)
        {
            Equipment equipment = request.ToEquipment();

            var validationResult = await _equipmentValidator.ValidateUpdateEntity(equipment, equipmentId);
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
            
            var validationResult = await _equipmentValidator.ValidateNewEntity(equipment);
            if (validationResult.IsFailure)
                return Result.Failure<EquipmentResponse>(validationResult.Error);

            var newEquipment = await _equipmentRepository.AddEquipmentAsync(equipment);
            return newEquipment.ToEquipmentResponse();
        }

        public async Task<Result<bool>> DoesEquipmentExist(Guid equipmentId)
        {
            var exists = await _equipmentRepository.DoesEquipmentExistsAsync(equipmentId);

            if (!exists)
                return Result.Failure<bool>(EquipmentErrors.EquipmentNotFound);

            return exists;
        }

            

    }
}
