namespace My.ZhiCore.CAP;

[DependsOn(
    typeof(AbpEventBusModule), 
    typeof(ZhiCoreLocalizationModule),
    typeof(AbpUnitOfWorkModule))]
public class ZhiCoreCapModule : AbpModule
{
}