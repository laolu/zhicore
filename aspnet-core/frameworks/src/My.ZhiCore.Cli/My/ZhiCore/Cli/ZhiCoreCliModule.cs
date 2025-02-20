namespace My.ZhiCore.Cli;

[DependsOn(
    typeof(My.ZhiCore.Cli.ZhiCoreCliCoreModule),
    typeof(AbpAutofacModule)
)]
public class ZhiCoreCliModule : AbpModule
{
}
