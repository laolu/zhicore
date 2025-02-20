namespace My.ZhiCore.EntityFrameworkCore
{
    public class EntityFrameworkCoreZhiCoreDbSchemaMigrator
        : IZhiCoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreZhiCoreDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ZhiCoreMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ZhiCoreDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}