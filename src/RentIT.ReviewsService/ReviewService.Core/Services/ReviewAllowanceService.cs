using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.ReviewAllowance;
using ReviewService.Core.Mappings;
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

    public async Task AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken)
    {
        var allowanceToAdd = request.ToReviewAllowance();

        //if allowace already exists, exit early
        if (!await _reviewAllowanceRepository.IsAllowanceUnique(allowanceToAdd))
            return;

        await _reviewAllowanceRepository.AddAllowanceAsync(allowanceToAdd, cancellationToken);
    }

    public async Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken)
    {
        var allowance = await _reviewAllowanceRepository.GetAllowanceById(id, cancellationToken);

        return allowance is null 
            ? Result.Failure<ReviewAllowanceResponse>(Error.NotFound) 
            : allowance.ToReviewAllowanceResponse();
    }

    public async Task<Result> DeleteAllowance(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewAllowanceRepository.DeleteAllowanceAsync(id, cancellationToken);
        return result 
            ? Result.Success() 
            : Result.Failure(Error.NotFound);
    }

    public async Task<Result<IEnumerable<ReviewAllowanceResponse>>> GetAllReviewAllowances(CancellationToken cancellationToken = default)
    {
        var result = await _reviewAllowanceRepository.GetAllReviewAllowances(cancellationToken);

        return Result.Success(result.Select(item => item.ToReviewAllowanceResponse()));
    }

}
