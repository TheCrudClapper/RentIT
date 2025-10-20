using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Infrastructure.DbContexts;
using ReviewServices.Infrastructure.Repositories;

namespace ReviewService.Infrastructure.Repositories;

public class ReviewRepository : BaseReviewRepository, IReviewRepository
{
    public ReviewRepository(ReviewsDbContext context) : base(context)
    {
    }
}
