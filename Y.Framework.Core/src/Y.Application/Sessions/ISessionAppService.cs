using System.Threading.Tasks;
using Abp.Application.Services;
using Y.Sessions.Dto;

namespace Y.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
