using My.ZhiCore.BasicManagement;
using My.ZhiCore.BasicManagement.Localization;
using My.ZhiCore.Core;
using My.ZhiCore.FileManagement;
using My.ZhiCore.LanguageManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreCoreModule),
        typeof(BasicManagementDomainSharedModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(FileManagementDomainSharedModule)
    )]
    public class ZhiCoreDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ZhiCoreGlobalFeatureConfigurator.Configure();
            ZhiCoreModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<ZhiCoreDomainSharedModule>(ZhiCoreDomainSharedConsts.NameSpace); });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ZhiCoreResource>(ZhiCoreDomainSharedConsts.DefaultCultureName)
                    .AddVirtualJson("/Localization/ZhiCore")
                    .AddBaseTypes(typeof(BasicManagementResource))
                    .AddBaseTypes(typeof(AbpTimingResource));

                options.DefaultResourceType = typeof(ZhiCoreResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace(ZhiCoreDomainSharedConsts.NameSpace, typeof(ZhiCoreResource)); });
        }
    }
}