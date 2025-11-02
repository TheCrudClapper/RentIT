using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts;

public interface IUserService
{
    /// <summary>
    /// Asynchronously retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{UserDTO}"/>
    /// representing the outcome of the retrieval, including the user data if found.</returns>
    Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Asynchronously retrieves all users from the data source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of user responses for
    /// all users found.</returns>
    Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Asynchronously retrieves  info from db based of collection of Id's.
    /// Used in microservice - microservice communication
    /// </summary>
    /// <param name="userIds">Collection of user ids to fetch info about</param>
    /// <param name="cancellationToken">Used for canceling asynchronous operation</param>
    /// <returns></returns>
    Task<IEnumerable<UserDTO>> GetUsersByUserId(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}

