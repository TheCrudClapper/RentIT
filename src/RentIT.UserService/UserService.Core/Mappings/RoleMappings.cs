using UserService.Core.Domain.Entities;
using UserService.Core.Enums;

namespace UserService.Core.Mappings;

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
