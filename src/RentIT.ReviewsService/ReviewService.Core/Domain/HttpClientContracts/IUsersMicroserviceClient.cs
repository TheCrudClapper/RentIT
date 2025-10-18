using ReviewService.Core.DTO;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IUsersMicroserviceClient
{
    public Task<Result<UserResponse>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
