using ReviewService.Core.DTO.User;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IUsersMicroserviceClient
{
    public Task<Result<UserResponse>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<UserResponse>>> GetUsersByUsersIdAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}
