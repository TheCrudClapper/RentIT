using UserService.Core.Domain.Entities;
using UserService.Core.DTO.UserDto;
using UserService.Core.Enums;

namespace UserService.Core.Mappings;
/// <summary>
/// This class is used as source of mappings between dto =><= entity
/// </summary>
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

    public static UserDTO ToUserDTO(this User user, IList<string> roles)
    {
        string role = roles.Contains(UserRoleOption.Admin.ToString())
            ? UserRoleOption.Admin.ToString()
            : UserRoleOption.User.ToString();

        return new UserDTO(user.Id, user.Email!,role);
    }
}

