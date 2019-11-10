using System.Threading.Tasks;
using Abp.Application.Services;
using Y.Authorization.Accounts.Dto;

namespace Y.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
