using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts
{
    public interface IUserRentalService
    {
        Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(Guid userId);
        Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId);
        Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId);
        Task<Result> UpdateRental(Guid rentalId, UserRentalUpdateRequest request, Guid userId);
        Task<Result> DeleteRental(Guid rentalId, Guid userId);
    }
}
