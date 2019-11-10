using System.Threading.Tasks;
using Y.Configuration.Dto;

namespace Y.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
