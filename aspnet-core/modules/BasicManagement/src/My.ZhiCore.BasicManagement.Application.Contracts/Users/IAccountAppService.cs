using My.ZhiCore.BasicManagement.Users.Dtos;

namespace My.ZhiCore.BasicManagement.Users
{
    public interface IAccountAppService: IApplicationService
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        Task<LoginOutput> LoginAsync(LoginInput input);
    }
}
