namespace My.ZhiCore.Data
{
    public interface IZhiCoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
