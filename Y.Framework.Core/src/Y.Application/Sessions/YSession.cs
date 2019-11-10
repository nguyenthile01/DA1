using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Y.Authorization.Users;
using Y.Core;
using Y.Sessions.Dto;

namespace Y.Sessions
{
    public class YSession : ClaimsAbpSession, ITransientDependency
    {
        public YSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider) :
            base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {

        }

        //public string UserCookie
        //{
        //    get
        //    {
        //        var userCookieClaim =
        //            PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_UserCookie");
        //        if (string.IsNullOrEmpty(userCookieClaim?.Value))
        //        {
        //            var cookie = CommonHelpers.RandomString(20);
        //            PrincipalAccessor.Principal.?.Claims.FirstOrDefault(c => c.Type == "Application_UserCookie");
        //        }

        //        return userCookieClaim.Value;
        //    }
        //}
        //public async Task<ClaimsPrincipal> CreateAsync(User user)
        //{
        //    var claim = await base.CreateAsync(user);
        //    claim.Identities.First().AddClaim(new Claim("Application_UserEmail", user.EmailAddress));

        //    return claim;
        //}
    }
}
