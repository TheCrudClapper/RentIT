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
            };
        }
    }
}
