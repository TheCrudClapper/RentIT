using RentalService.Core.Domain.ResultTypes;
using RentIT.Contracts.DTO.RentalMicroservice.Rentals;

namespace RentalService.Core.Domain.HtppClientContracts;

public interface IEquipmentMicroserviceClient
{
    Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken);
    Task<Result<IReadOnlyCollection<EquipmentResponse>>> GetEquipmentsByIds(IEnumerable<Guid> equipmentIds, CancellationToken cancellationToken);
}
