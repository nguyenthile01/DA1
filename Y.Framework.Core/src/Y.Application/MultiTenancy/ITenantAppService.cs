using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Y.MultiTenancy.Dto;

namespace Y.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

