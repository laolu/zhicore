using My.ZhiCore.BasicManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace My.ZhiCore.BasicManagement;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(BasicManagementEntityFrameworkCoreTestModule)
    )]
public class BasicManagementDomainTestModule : AbpModule
{

}
