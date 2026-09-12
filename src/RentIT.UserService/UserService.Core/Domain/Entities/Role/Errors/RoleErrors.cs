using UserService.Core.Domain.ResultTypes;

namespace UserService.Core.Domain.Entities.Role.Errors
{
    public class RoleErrors
    {
        public static readonly Error NotFound = new Error(
           ErrorType.NotFound, "Role.NotFound", "This role cannot be assigned during registration");

    }
}
