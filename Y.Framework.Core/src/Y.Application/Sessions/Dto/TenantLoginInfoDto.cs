using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Y.MultiTenancy;

namespace Y.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
