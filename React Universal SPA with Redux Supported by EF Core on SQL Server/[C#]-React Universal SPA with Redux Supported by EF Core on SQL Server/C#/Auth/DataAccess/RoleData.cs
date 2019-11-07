using Microsoft.AspNetCore.Identity;

namespace Auth.DataAccess
{
    public static class RoleData
    {
        public static string UserRoleName { get; } = "User";

        public static IdentityRole UserRole { get; } = new IdentityRole(UserRoleName);
    }
}
