using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.ReviewAllowance;
using ReviewService.Core.Mappings;
using ReviewService.Core.ResultTypes;
using ReviewService.Core.ServiceContracts;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.Services;

public class ReviewAllowanceService : IReviewAllowanceService
{
    private readonly IReviewAllowanceRepository _reviewAllowanceRepository;
    public ReviewAllowanceService(IReviewAllowanceRepository reviewAllowanceRepository)
    {
        _reviewAllowanceRepository = reviewAllowanceRepository;
    }

    public Task<Result<ReviewAllowanceResponse>> AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken = default)
    {
        var allowance = await _reviewAllowanceRepository.GetAllowanceById(id, cancellationToken);

        return allowance is null 
            ? Result.Failure<ReviewAllowanceResponse>(Error.NotFound) 
            : allowance.ToReviewAllowanceResponse();
    }

    public async Task<Result> RevokeAllowance(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewAllowanceRepository.RevokeAllowanceAsync(id, cancellationToken);
        return result 
            ? Result.Success() 
            : Result.Failure(Error.NotFound);
    }
}
