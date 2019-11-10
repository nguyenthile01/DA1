using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Y.Authorization.Roles;

namespace Y.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }
        //public override async Task<ClaimsPrincipal> CreateAsync(User user)
        //{
        //    var claim = await base.CreateAsync(user);
        //    claim.Identities.First().AddClaim(new Claim("Application_UserEmail", user.EmailAddress));

        //    return claim;
        //}
    }
}
