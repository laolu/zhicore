// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZhiCoreCapOptionsExtensions
    {
        public static CapOptions SetCapDbConnectionString(this CapOptions options, string dbConnectionString)
        {
            options.RegisterExtension(new ZhiCoreEfCoreDbContextCapOptionsExtension
            {
                CapUsingDbConnectionString = dbConnectionString
            });
            
            return options;
        }
    }
}
