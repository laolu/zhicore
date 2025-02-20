using My.ZhiCore.BasicManagement;
using My.ZhiCore.FileManagement;
using My.ZhiCore.LanguageManagement;
using My.ZhiCore.NotificationManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreApplicationContractsModule),
        typeof(BasicManagementHttpApiClientModule),
        typeof(DataDictionaryManagementHttpApiClientModule),
        typeof(NotificationManagementHttpApiClientModule),
        typeof(LanguageManagementHttpApiClientModule),
        typeof(FileManagementHttpApiClientModule)
    )]
    public class ZhiCoreHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ZhiCoreApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
