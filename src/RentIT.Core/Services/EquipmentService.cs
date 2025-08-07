using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Core.DTO.EquipmentDto;
using RentIT.Core.Mappings;
using RentIT.Core.ResultTypes;
using RentIT.Core.ServiceContracts;

namespace RentIT.Core.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
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
            
            bool updationResult = await _equipmentRepository.UpdateEquipmentAsync(equipmentId, equipment);

            if (!updationResult)
                return Result.Failure(EquipmentErrors.EquipmentNotFound);

            return Result.Success();
        }

        public async Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request)
        {
            Equipment equipment = request.ToEquipment();
            
            var newEquipment = await _equipmentRepository.AddEquipmentAsync(equipment);

            return newEquipment.ToEquipmentResponse();
        }
    }
}
