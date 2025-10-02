using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;

public interface IRentalService
{
    Task<Result> DeleteRentalByEquipmentId(Guid equipmentId, CancellationToken cancellationToken);
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, CancellationToken cancellationToken);
    Task<Result<RentalResponse>> AddRental(RentalAddRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteRental(Guid rentalId, CancellationToken cancellationToken);
}