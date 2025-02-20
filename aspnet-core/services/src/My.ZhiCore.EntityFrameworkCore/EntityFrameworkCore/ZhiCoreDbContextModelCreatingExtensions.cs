using Volo.Abp.EntityFrameworkCore.Modeling;

namespace My.ZhiCore.EntityFrameworkCore;

public static class ZhiCoreDbContextModelCreatingExtensions
{
    public static void ConfigureZhiCore(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}