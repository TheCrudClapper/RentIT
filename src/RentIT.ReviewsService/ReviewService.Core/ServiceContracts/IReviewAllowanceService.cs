using RentIT.Contracts.DTO.ReviewMicroservice.ReviewAllowances;
using ReviewService.Core.Domain.ResultTypes;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewAllowanceService
{
    Task<Result<IReadOnlyCollection<ReviewAllowanceResponse>>> GetAllReviewAllowances(CancellationToken cancellationToken = default);
    Task AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAllowance(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken = default);
}
