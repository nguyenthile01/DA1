using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Y.Configuration;
using Y.Authentication.External;
using Y.Authentication.External.Facebook;
using Y.Authentication.External.Google;
//using Abp.AspNetZeroCore.Web.Authentication.External.Facebook;
//using Abp.AspNetZeroCore.Web.Authentication.External.Google;
//using Abp.AspNetZeroCore.Web.Authentication.External;

namespace Y.Web.Host.Startup
{
    [DependsOn(
       typeof(YWebCoreModule))]
    public class YWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public YWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            ConfigureExternalAuthProviders();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<IExternalAuthConfiguration>();

            //if (bool.Parse(_appConfiguration["Authentication:OpenId:IsEnabled"]))
            //{
            //    externalAuthConfiguration.Providers.Add(
            //        new ExternalLoginProviderInfo(
            //            OpenIdConnectAuthProviderApi.Name,
            //            _appConfiguration["Authentication:OpenId:ClientId"],
            //            _appConfiguration["Authentication:OpenId:ClientSecret"],
            //            typeof(OpenIdConnectAuthProviderApi),
            //            new Dictionary<string, string>
            //            {
            //                {"Authority", _appConfiguration["Authentication:OpenId:Authority"]},
            //                {"LoginUrl",_appConfiguration["Authentication:OpenId:LoginUrl"]}
            //            }
            //        )
            //    );
            //}

            if (bool.Parse(_appConfiguration["Authentication:Facebook:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        FacebookAuthProviderApi.Name,
                        _appConfiguration["Authentication:Facebook:AppId"],
                        _appConfiguration["Authentication:Facebook:AppSecret"],
                        typeof(FacebookAuthProviderApi)
                    )
                );
            }

            if (bool.Parse(_appConfiguration["Authentication:Google:IsEnabled"]))
            {
                externalAuthConfiguration.Providers.Add(
                    new ExternalLoginProviderInfo(
                        GoogleAuthProviderApi.Name,
                        _appConfiguration["Authentication:Google:ClientId"],
                        _appConfiguration["Authentication:Google:ClientSecret"],
                        typeof(GoogleAuthProviderApi)
                    )
                );
            }

            ////not implemented yet. Will be implemented with https://github.com/aspnetzero/aspnet-zero-angular/issues/5
            //if (bool.Parse(_appConfiguration["Authentication:Microsoft:IsEnabled"]))
            //{
            //    externalAuthConfiguration.Providers.Add(
            //        new ExternalLoginProviderInfo(
            //            MicrosoftAuthProviderApi.Name,
            //            _appConfiguration["Authentication:Microsoft:ConsumerKey"],
            //            _appConfiguration["Authentication:Microsoft:ConsumerSecret"],
            //            typeof(MicrosoftAuthProviderApi)
            //        )
            //    );
            //}
        }
    }
}
