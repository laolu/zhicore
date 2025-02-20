using Volo.Abp.Testing;

namespace My.ZhiCore.ElasticSearch
{

    public abstract class ZhiCoreElasticSearchTestBase : AbpIntegratedTest<ZhiCoreElasticSearchTestBaseModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
