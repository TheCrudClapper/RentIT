using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;
/// <summary>
/// Defines the contract for managing equipment rental operations, including creating, retrieving, updating, and
/// deleting rental records, as well as marking equipment as returned.
/// </summary>
/// <remarks>Implementations of this interface should ensure thread safety if accessed concurrently. All methods
/// are asynchronous and support cancellation via the provided cancellation token. The interface abstracts rental
/// management functionality, allowing for different underlying data stores or business logic implementations.</remarks>
public interface IRentalService
{
    Task<Result> DeleteRentalByEquipmentId(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> AddRental(RentalAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteRental(Guid rentalId, CancellationToken cancellationToken = default);
    Task<Result> MarkEquipmentAsReturned(Guid rentalId, ReturnEquipmentRequest request, CancellationToken cancellationToken = default);
}