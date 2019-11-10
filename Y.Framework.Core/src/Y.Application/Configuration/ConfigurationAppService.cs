using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Y.Configuration.Dto;

namespace Y.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : YAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
