using Volo.Abp;
using Volo.Abp.Testing;

namespace My.ZhiCore.Core.Cli
{
    public abstract class  ZhiCoreCoreCliTestBase : AbpIntegratedTest<ZhiCoreCoreCliTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}