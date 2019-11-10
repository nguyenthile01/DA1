using Abp.Application.Services;

namespace Y.Services
{
    public interface ITemplateRenderer : IApplicationService
    {
        string Parse<T>(string template, T model);
    }
}
