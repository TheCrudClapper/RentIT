using RentIT.Core.Domain.Entities;
using RentIT.Core.Enums;

namespace RentIT.Core.Mappings
{
    public static class RoleMappings
    {
        public static Role ToRoleEntity(this UserRoleOption roleOption)
        {
            return new Role
            {
                Name = roleOption.ToString(),
            };
        }
    }
}
