using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.EntityFrameworkCore.Tests.Entities.Blogs;

public interface IBlogRepository : IBasicRepository<Blog, Guid>
{
    Task<List<Blog>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

    Task<long> GetCountAsync();
}