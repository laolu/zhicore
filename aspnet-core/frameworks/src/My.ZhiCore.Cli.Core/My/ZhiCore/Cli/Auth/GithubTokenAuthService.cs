namespace My.ZhiCore.Cli.Auth;

public class GithubTokenAuthService : ITokenAuthService, ITransientDependency
{
    public async Task SetAsync(string token)
    {
        if (token.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException("token不能为空");
        }

        if (!Directory.Exists(CliPaths.Root))
        {
            Directory.CreateDirectory(CliPaths.Root);
        }

        await File.WriteAllTextAsync(CliPaths.AccessToken, token, Encoding.UTF8);
    }

    public async Task<string> GetAsync()
    {
        if (!File.Exists(CliPaths.AccessToken))
        {
            return string.Empty;
        }
        return await File.ReadAllTextAsync(CliPaths.AccessToken);
    }
}