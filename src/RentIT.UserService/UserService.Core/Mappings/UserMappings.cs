using UserService.Core.Domain.Entities;
using UserService.Core.DTO;
using UserService.Core.DTO.UserDto;

namespace UserService.Core.Mappings;
public static class UserMappings
{
    public static User ToUserEntity(this RegisterRequest request)
    {
        return new User
        {
            //Email is used as user's username
            Id = new Guid(),
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            IsActive = true,
            DateCreated = DateTime.UtcNow,
        };
    }
    public static UserResponse ToUserResponse(this User user)
    {
        //Email is always added
        return new UserResponse(
            user.Id, user.FirstName, user.LastName, user.Email!);
    }
}

