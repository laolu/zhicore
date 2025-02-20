namespace My.ZhiCore.FileManagement.Data;

public interface IFileManagementDbSchemaMigrator
{
    Task MigrateAsync();
}