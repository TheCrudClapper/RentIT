using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.UserDto;

namespace RentIT.Core.Mappings
{
    public static class UserMappings
    {
        public static User ToUserEntity(this RegisterRequest request)
        {
            return new User
            {
                //Email is used as user's username
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
            return new UserResponse
            {
                //Email is always added in register 
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
        }
    }
}
