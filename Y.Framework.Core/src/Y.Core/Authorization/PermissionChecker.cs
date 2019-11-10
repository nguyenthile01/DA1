using Abp.Authorization;
using Y.Authorization.Roles;
using Y.Authorization.Users;

namespace Y.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
