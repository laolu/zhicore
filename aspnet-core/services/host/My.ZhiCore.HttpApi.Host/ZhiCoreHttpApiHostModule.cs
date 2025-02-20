using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.DistributedLocking;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreHttpApiModule),
        typeof(ZhiCoreSharedHostingMicroserviceModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(ZhiCoreEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAccountWebModule),
        typeof(ZhiCoreApplicationModule),
        typeof(ZhiCoreCapModule),
        typeof(ZhiCoreCapEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpDistributedLockingModule),
        typeof(AbpBlobStoringFileSystemModule)
        //typeof(AbpBackgroundJobsHangfireModule)
    )]
    public partial class ZhiCoreHttpApiHostModule : AbpModule
    {
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用程序初始化的时候注册hangfire
            //context.CreateRecurringJob();
            base.OnPostApplicationInitialization(context);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ConfigureCache(context);
            ConfigurationDistributedLocking(context);
            ConfigureSwaggerServices(context);
            ConfigureJwtAuthentication(context, configuration);
            //ConfigureHangfire(context);
            ConfigureMiniProfiler(context);
            ConfigureIdentity(context);
            ConfigureCap(context);
            ConfigureAuditLog(context);
            ConfigurationSignalR(context);
            ConfigurationMultiTenancy();
            ConfigureBlobStorage();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
            app.UseZhiCoreRequestLocalization();
            app.UseCorrelationId();
            app.MapAbpStaticAssets();
            if (configuration.GetValue("MiniProfiler:Enabled", false))
            {
                app.UseMiniProfiler();
            }

            app.UseRouting();
            app.UseCors(ZhiCoreHttpApiHostConst.DefaultCorsPolicyName);
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ZhiCore/swagger.json", "ZhiCore API");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseUnitOfWork();
            
            app.UseConfiguredEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health"); 
                
                // endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions()
                // {
                //     Authorization = new[] { new CustomHangfireAuthorizeFilter() },
                //     IgnoreAntiforgeryToken = true
                // });

            });
       
            if (configuration.GetValue("Consul:Enabled", false))
            {
                app.UseConsul();
            }
        }
    }
}