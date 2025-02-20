using Volo.Abp;
using Volo.Abp.Testing;

namespace My.ZhiCore
{

    public abstract class ZhiCoreLocalizationTestBase : AbpIntegratedTest<ZhiCoreLocalizationTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
