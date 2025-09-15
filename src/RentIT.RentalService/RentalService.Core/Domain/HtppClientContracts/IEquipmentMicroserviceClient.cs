using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.Domain.HtppClientContracts
{
    public interface IEquipmentMicroserviceClient
    {
        Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId);
        Task<Result<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds);
    }
}
