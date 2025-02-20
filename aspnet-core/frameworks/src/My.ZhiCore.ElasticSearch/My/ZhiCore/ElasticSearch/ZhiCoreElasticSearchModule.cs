namespace My.ZhiCore.ElasticSearch;

[DependsOn(typeof(AbpAutofacModule))]
public class ZhiCoreElasticSearchModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<ZhiCoreElasticSearchOptions>(context.Services.GetConfiguration().GetSection("ElasticSearch"));
    }
}