namespace My.ZhiCore.Cli.Auth;

public interface IConfigService
{
    /// <summary>
    /// 设置token
    /// </summary>
    Task SetAsync(string config);
    
    /// <summary>
    /// 获取token
    /// </summary>
    Task<ConfigOptions> GetAsync();
}