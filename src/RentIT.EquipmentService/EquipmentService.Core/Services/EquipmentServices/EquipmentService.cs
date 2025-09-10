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
        private readonly IRentalMicroserviceClient _rentalMicroserviceClient;
        public EquipmentService(IEquipmentRepository equipmentRepository,
            IEquipmentValidator equipmentValidator,
            IRentalMicroserviceClient rentalMicroserviceClient)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentValidator = equipmentValidator;
            _rentalMicroserviceClient = rentalMicroserviceClient;
        }

        public async Task<Result> DeleteEquipment(Guid equipmentId)
        {
            var deletionResult = await _equipmentRepository.DeleteEquipmentAsync(equipmentId);

            if(!deletionResult)
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            var rentalDeletionResult = await _rentalMicroserviceClient.DeleteRentalsByEquipmentId(equipmentId);
            if (rentalDeletionResult.IsFailure)
                return Result.Failure(rentalDeletionResult.Error);

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

        public async Task<IEnumerable<EquipmentResponse>> GetAllEquipmentsByIds(IEnumerable<Guid> equipmentIds)
        {
            var equipmentsByCondition = await _equipmentRepository.GetEquipmentsByCondition(item => equipmentIds.Contains(item.Id));

            return equipmentsByCondition.Select(equipment => equipment.ToEquipmentResponse()).ToList();
        }
    }
}
