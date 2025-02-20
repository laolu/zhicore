using My.ZhiCore.Core;

namespace My.ZhiCore.LanguageManagement
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(ZhiCoreCoreModule)
    )]
    public class LanguageManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<LanguageManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<LanguageManagementResource>(LanguageManagementConsts.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/LanguageManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(LanguageManagementConsts.NameSpace, typeof(LanguageManagementResource));
            });
        }
    }
}
