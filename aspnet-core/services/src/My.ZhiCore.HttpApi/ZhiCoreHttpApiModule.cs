using My.ZhiCore.BasicManagement;
using My.ZhiCore.FileManagement;
using My.ZhiCore.LanguageManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreApplicationContractsModule),
        typeof(BasicManagementHttpApiModule),
        typeof(DataDictionaryManagementHttpApiModule),
        typeof(NotificationManagementHttpApiModule),
        typeof(LanguageManagementHttpApiModule),
        typeof(FileManagementHttpApiModule)
        )]
    public class ZhiCoreHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ZhiCoreResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
