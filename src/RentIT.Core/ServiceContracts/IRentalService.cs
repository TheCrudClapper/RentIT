using RentIT.Core.DTO.RentalDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalResponse>> GetAllActiveRentals();
        Task<Result<RentalResponse>> GetActiveRental(Guid rentalId);
        Task<Result<RentalResponse>> AddRental(RentalAddRequest request);
        Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request);
        Task<Result> DeleteRental(Guid rentalId);
    }
}
