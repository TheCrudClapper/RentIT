using RentIT.Core.DTO.RentalDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalResponse>> GetAllRentals();
        Task<Result<RentalResponse>> GetRental(Guid rentalId);
        Task<Result<RentalResponse>> AddRental(RentalAddRequest request);
        Task<Result> UpdateRental(Guid rentalId, RentalUpdateRequest request);
        Task<Result> DeleteRental(Guid rentalId);
    }
}
