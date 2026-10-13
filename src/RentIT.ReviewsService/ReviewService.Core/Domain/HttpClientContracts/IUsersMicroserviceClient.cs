using RentIT.Contracts.DTO.Shared;
using ReviewService.Core.Domain.ResultTypes;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IUsersMicroserviceClient
{
    public Task<Result<UserDTO>> GetUserByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Result<IReadOnlyCollection<UserDTO>>> GetUsersByUsersIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}
