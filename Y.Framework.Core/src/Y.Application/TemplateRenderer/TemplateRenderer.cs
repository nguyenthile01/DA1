using RazorLight;
using System;
using System.Threading.Tasks;

namespace Y.Services
{
    public class TemplateRenderer : ITemplateRenderer
    {

        public string Parse<T>(string template, T model)
        {
            return ParseAsync(template, model).GetAwaiter().GetResult();
        }

        async Task<string> ParseAsync<T>(string template, T model)
        {
            var engine = new RazorLightEngineBuilder().Build();

            return await engine.CompileRenderAsync<T>(Guid.NewGuid().ToString(), template, model);
        }
    }
}

