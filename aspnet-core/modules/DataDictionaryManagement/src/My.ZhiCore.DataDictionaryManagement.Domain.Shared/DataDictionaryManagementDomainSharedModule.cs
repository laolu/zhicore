using My.ZhiCore.Core;

namespace My.ZhiCore.DataDictionaryManagement
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(ZhiCoreCoreModule)
    )]
    public class DataDictionaryManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DataDictionaryManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DataDictionaryManagementResource>(DataDictionaryManagementConsts.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/DataDictionaryManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(DataDictionaryManagementConsts.NameSpace, typeof(DataDictionaryManagementResource));
            });
        }
    }
}
