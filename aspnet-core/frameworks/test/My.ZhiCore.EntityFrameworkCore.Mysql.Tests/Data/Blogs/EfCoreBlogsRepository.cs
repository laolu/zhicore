using My.ZhiCore.EntityFrameworkCore.Tests.Blogs;
using My.ZhiCore.EntityFrameworkCore.Tests.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace My.ZhiCore.EntityFrameworkCore.Tests.Data.Blogs;

/// <summary>
/// 博客 仓储Ef core 实现
/// </summary>
public class EfCoreBlogRepository :
    EfCoreRepository<TestsDbContext, Blog, Guid>,
    IBlogRepository
{
    public EfCoreBlogRepository(IDbContextProvider<TestsDbContext> dbContextProvider) : base(dbContextProvider)
      {
      }

        public async Task<List<Blog>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
        {
            return await (await GetDbSetAsync())
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        public async Task<long> GetCountAsync()
        {
            return await (await GetDbSetAsync()).CountAsync();
        }
        
        public override async Task<IQueryable<Blog>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
        
        
}    