using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Y.Authorization.Users;
using Y.Roles.Dto;

namespace Y.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserMobileDto
    {

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        public List<string> RoleNames { get; set; }

        public long UserId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public int AvatarId { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public List<string> Permissions { get; set; }
    }
}
