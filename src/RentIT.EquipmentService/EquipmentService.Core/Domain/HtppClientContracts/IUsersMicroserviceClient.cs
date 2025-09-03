using EquipmentService.Core.DTO.UserDto;
using EquipmentService.Core.ResultTypes;
namespace EquipmentService.Core.Domain.HtppClientContracts;

/// <summary>
/// Defines methods for interacting with the Users microservice.
/// </summary>
/// <remarks>This interface provides functionality to retrieve user information from the Users
/// microservice. Implementations of this interface are expected to handle communication with the microservice and
/// return user data in the form of a <see cref="UserDTO"/>.</remarks>
public interface IUsersMicroserviceClient
{
    Task<Result<UserDTO?>> GetUserByUserId(Guid userId);
}

