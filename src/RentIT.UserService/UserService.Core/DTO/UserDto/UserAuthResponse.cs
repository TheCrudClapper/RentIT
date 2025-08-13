namespace UserService.Core.DTO.UserDto;

public record UserAuthResponse(Guid userId, string Email, string Token);

