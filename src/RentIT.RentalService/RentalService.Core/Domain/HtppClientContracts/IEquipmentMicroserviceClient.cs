using RentalService.Core.Domain.ResultTypes;
using RentalService.Core.DTO.Rentals;

namespace RentalService.Core.Domain.HtppClientContracts;

public interface IEquipmentMicroserviceClient
{
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken);
}
