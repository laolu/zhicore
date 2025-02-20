using Volo.Abp.DependencyInjection;

namespace My.ZhiCore.BasicManagement.Data
{
    /* This is used if database provider does't define
     * IZhiCoreDbSchemaMigrator implementation.
     */
    public class NullZhiCoreDbSchemaMigrator : IZhiCoreDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}