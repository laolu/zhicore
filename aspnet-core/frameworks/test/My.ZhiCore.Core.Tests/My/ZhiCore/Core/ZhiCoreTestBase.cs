using Volo.Abp;
using Volo.Abp.Testing;

namespace My.ZhiCore.Core
{
    public abstract class  ZhiCoreTestBase : AbpIntegratedTest<ZhiCoreTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}