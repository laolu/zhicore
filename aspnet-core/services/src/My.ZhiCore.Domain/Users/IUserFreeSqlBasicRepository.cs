namespace My.ZhiCore.Users
{
    public interface IUserFreeSqlBasicRepository
    {
        Task<List<UserOutput>> GetListAsync();
    }
}
