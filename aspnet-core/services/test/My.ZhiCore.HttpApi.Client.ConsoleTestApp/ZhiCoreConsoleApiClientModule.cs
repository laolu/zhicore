namespace My.ZhiCore.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(ZhiCoreHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ZhiCoreConsoleApiClientModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(
                        policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                    );
                });
            });
        }
    }
}
