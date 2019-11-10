using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Extensions;
using Y.Core;

namespace Y.Authorization.Users
{
    public class User : AbpUser<User>
    {
        #region Properties
        public int AvatarId { get; set; }
        [MaxLength(EntityLength.Phone)]
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        [MaxLength(EntityLength.LastName)]
        public override string Surname { get; set; }
        [MaxLength(EntityLength.LastName)]
        public override string Name { get; set; }


        #endregion

        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
