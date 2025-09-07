namespace RentalService.Core.DTO.UserDto
{
    /// <summary>
    /// This record is being used while calling other microservices
    /// in synchronous communication
    /// </summary>
    /// <param name="Id">Id of an User</param>
    /// <param name="Email">Email of User</param>
    /// <param name="Role">Role of User</param>
    public record UserDTO(Guid Id, string Email, string Role);
}
