using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.User;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IUsersMicroserviceClient
{
    public Task<Result<UserDTO>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<UserDTO>>> GetUsersByUsersIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}
