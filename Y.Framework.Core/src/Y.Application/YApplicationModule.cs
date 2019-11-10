using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Dependency;
using Abp.MailKit;
using Abp.Modules;
using Abp.Quartz;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Y.Authorization;
using Y.Core;
using Y.Dto;
using Y.Services;

namespace Y
{
    [DependsOn(
        typeof(YCoreModule),
        typeof(AbpMailKitModule),
        typeof(AbpQuartzModule),
        typeof(AbpAutoMapperModule))]
    public class YApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
            Configuration.Authorization.Providers.Add<YAuthorizationProvider>();
            IocManager.Register<IPictureAppService, PictureAppService>(DependencyLifeStyle.Transient);
            IocManager.Register<INopFileProvider, NopFileProvider>(DependencyLifeStyle.Transient);
            IocManager.Register<IDownloadService, DownloadService>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(YApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg =>
                {
                    cfg.AddProfiles(thisAssembly);

                    //cfg.CreateMap<PaymentResult, PaymentResultDto>();
                }
            );

            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));

            });
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}