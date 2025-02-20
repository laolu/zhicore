using My.ZhiCore.Cli.Dto;

namespace My.ZhiCore.Cli;

public interface ICodeService
{
    Task<string> GetAccessTokenAsync();

    Task<string> DownloadAsync(string accessToken, Guid projectId, Guid templateId, List<Guid> entityId);

    Task<GetProjectAndEntityResponse> GetProjectAndEntityAsync(string accessToken, Guid projectId);


    Task CheckHealthAsync(string url);
    
    Task<FindTenantResponse> FindTenantAsync(string url, string tenantName);
    
    Task<string> LoginAsync(string url, string userName, string password);
}