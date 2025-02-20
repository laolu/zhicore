using Volo.Abp.Modularity;

namespace My.ZhiCore.BasicManagement;

[DependsOn(
    typeof(BasicManagementApplicationModule),
    typeof(BasicManagementDomainTestModule)
    )]
public class BasicManagementApplicationTestModule : AbpModule
{

}
