using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.Domain.HtppClientContracts
{
    public interface IRentalMicroserviceClient
    {
        Task<Result> DeleteRentalsByEquipmentId(Guid equipmentId); 
    }
}
