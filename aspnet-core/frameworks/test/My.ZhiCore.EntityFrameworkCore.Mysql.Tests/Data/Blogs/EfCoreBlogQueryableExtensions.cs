using My.ZhiCore.EntityFrameworkCore.Tests.Blogs;
using My.ZhiCore.EntityFrameworkCore.Tests.Entities.Blogs;
using Microsoft.EntityFrameworkCore;

namespace My.ZhiCore.EntityFrameworkCore.Tests.Data.Blogs;

public static class EfCoreBlogQueryableExtensions
{
    public static IQueryable<Blog> IncludeDetails(this IQueryable<Blog> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            .Include(e => e.Posts)
            .ThenInclude(x => x.Comments);
    }
}