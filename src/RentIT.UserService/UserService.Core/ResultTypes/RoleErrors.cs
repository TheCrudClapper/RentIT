namespace UserService.Core.ResultTypes
{
    public class RoleErrors
    {
        public static readonly Error InvalidRole = new Error(
            400, "This role cannot be assigned during registration");

        public static readonly Error RoleCreationFailed = new Error(
            400, "Something went wrong while creating your role");

        public static readonly Error RoleAssignationFailed = new Error(
            400, "Something went wrong while assigning your role");

    }
}
