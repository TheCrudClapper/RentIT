using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts
{
    public interface IRentalService
    {
        Task<Result<IEnumerable<RentalResponse>>> GetAllRentals();
        Task<Result<RentalResponse>> GetRental(Guid rentalId);
        Task<Result<RentalResponse>> AddRental(RentalAddRequest request);
        Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request);
        Task<Result> DeleteRental(Guid rentalId);
    }
}
