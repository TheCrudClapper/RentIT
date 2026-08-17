namespace UserService.Core.DTO.UserDto;
public record UserResponse(Guid Id, string FirstName, string LastName, string Email, IList<string> Roles);

