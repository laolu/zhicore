namespace My.ZhiCore;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpLocalizationModule)
)]
public class ZhiCoreLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<ZhiCoreLocalizationModule>(ZhiCoreLocalizationConsts.NameSpace); });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<ZhiCoreLocalizationResource>(ZhiCoreLocalizationConsts.DefaultCultureName)
                .AddVirtualJson(ZhiCoreLocalizationConsts.DefaultLocalizationResourceVirtualPath);

            options.DefaultResourceType = typeof(ZhiCoreLocalizationResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace(ZhiCoreLocalizationConsts.NameSpace, typeof(ZhiCoreLocalizationResource)); });
    }
}