namespace My.ZhiCore.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ZhiCoreMigrationsDbContextFactory : IDesignTimeDbContextFactory<ZhiCoreDbContext>
    {
        public ZhiCoreDbContext CreateDbContext(string[] args)
        {
            ZhiCoreEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ZhiCoreDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);

            return new ZhiCoreDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath
                (
                    Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "../My.ZhiCore.DbMigrator/"
                    )
                )
                .AddJsonFile
                (
                    "appsettings.json",
                    false
                );

            return builder.Build();
        }
    }
}