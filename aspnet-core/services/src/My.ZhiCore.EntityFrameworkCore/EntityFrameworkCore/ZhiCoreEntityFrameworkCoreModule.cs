using My.ZhiCore.FileManagement.EntityFrameworkCore;
using My.ZhiCore.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace My.ZhiCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(ZhiCoreDomainModule),
        typeof(BasicManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(DataDictionaryManagementEntityFrameworkCoreModule),
        typeof(NotificationManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementEntityFrameworkCoreModule),
        typeof(FileManagementEntityFrameworkCoreModule)
        )]
    public class ZhiCoreEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ZhiCoreEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ZhiCoreDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            
            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also HayoonKoreaDbContextFactory for EF Core tooling.
                 *  https://github.com/abpframework/abp/issues/21879
                 * */
                options.UseMySQL(builder =>
                {
                    builder.TranslateParameterizedCollectionsToConstants();
                });
            });
        }
    }
}
