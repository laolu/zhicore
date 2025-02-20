using My.ZhiCore.Core;

namespace My.ZhiCore.NotificationManagement
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(ZhiCoreCoreModule)
    )]
    public class NotificationManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NotificationManagementResource>(NotificationManagementConsts.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/NotificationManagement");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(NotificationManagementConsts.NameSpace, typeof(NotificationManagementResource));
            });
        }
    }
}
