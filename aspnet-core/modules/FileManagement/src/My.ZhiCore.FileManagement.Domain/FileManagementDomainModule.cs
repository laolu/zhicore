using Volo.Abp.AutoMapper;

namespace My.ZhiCore.FileManagement;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule),
    typeof(FileManagementDomainSharedModule),
    typeof(AbpBlobStoringModule)
)]
public class FileManagementDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<FileManagementDomainModule>(); });
    }
}