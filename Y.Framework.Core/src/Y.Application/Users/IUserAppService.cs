using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Y.Roles.Dto;
using Y.Users.Dto;

namespace Y.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<UserDto> Register(CreateUserDto input);

        Task<UserMobileDto> GetProfile();

    }
}
