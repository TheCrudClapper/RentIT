using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;

public interface IRentalService
{
    Task<Result> DeleteRentalByEquipmentId(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> AddRental(RentalAddRequest request, string accessToken, CancellationToken cancellationToken = default);
    Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request, string accessToken, CancellationToken cancellationToken = default);
    Task<Result> DeleteRental(Guid rentalId, CancellationToken cancellationToken = default);
    Task<Result> MarkEquipmentAsReturned(ReturnEquipmentRequest request, CancellationToken cancellationToken = default);
}