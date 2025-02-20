namespace My.ZhiCore.FileManagement;

[DependsOn(
    typeof(FileManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class FileManagementApplicationContractsModule : AbpModule
{
}