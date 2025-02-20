using My.ZhiCore.BasicManagement.Features.Dtos;
using Volo.Abp.FeatureManagement;

namespace My.ZhiCore.BasicManagement.Features;

public interface IVoloFeatureAppService : IApplicationService
{
    /// <summary>
    /// 获取Features
    /// </summary>
    Task<GetFeatureListResultDto> GetAsync(GetFeatureListResultInput input);
    
    /// <summary>
    /// 更新Features
    /// </summary>
    Task UpdateAsync(UpdateFeatureInput input);

    /// <summary>
    /// 删除Features
    /// </summary>
    Task DeleteAsync(DeleteFeatureInput input);
}