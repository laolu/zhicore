namespace My.ZhiCore.FileManagement;

public static class FileManagementDbProperties
{
    public const string ConnectionStringName = "FileManagement";
    public static string DbTablePrefix { get; set; } = "Abp";

    public static string DbSchema { get; set; } = null;
}