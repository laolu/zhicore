namespace My.ZhiCore.FileManagement;

[DependsOn(
    typeof(FileManagementApplicationModule),
    typeof(FileManagementDomainTestModule)
)]
public class FileManagementApplicationTestModule : AbpModule
{
}