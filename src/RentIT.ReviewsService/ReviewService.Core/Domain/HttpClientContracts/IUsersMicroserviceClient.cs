using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Users;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IUsersMicroserviceClient
{
    public Task<Result<UserDTO>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Result<IReadOnlyCollection<UserDTO>>> GetUsersByUsersIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}
