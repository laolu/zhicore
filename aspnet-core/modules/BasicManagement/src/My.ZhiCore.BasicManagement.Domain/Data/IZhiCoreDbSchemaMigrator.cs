namespace My.ZhiCore.BasicManagement.Data
{
    public interface IZhiCoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
