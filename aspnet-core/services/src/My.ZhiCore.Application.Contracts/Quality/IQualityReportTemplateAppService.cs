using System;
using System.Threading.Tasks;
using My.ZhiCore.Quality.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量报表模板服务接口
    /// </summary>
    public interface IQualityReportTemplateAppService : IApplicationService
    {
        /// <summary>
        /// 获取报表模板列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>报表模板列表</returns>
        Task<PagedResultDto<QualityReportTemplateDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取报表模板详情
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns>报表模板详情</returns>
        Task<QualityReportTemplateDto> GetAsync(Guid id);

        /// <summary>
        /// 创建报表模板
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的报表模板</returns>
        Task<QualityReportTemplateDto> CreateAsync(CreateQualityReportTemplateDto input);

        /// <summary>
        /// 更新报表模板
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的报表模板</returns>
        Task<QualityReportTemplateDto> UpdateAsync(Guid id, UpdateQualityReportTemplateDto input);

        /// <summary>
        /// 删除报表模板
        /// </summary>
        /// <param name="id">模板ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取系统默认模板列表
        /// </summary>
        /// <returns>系统默认模板列表</returns>
        Task<ListResultDto<QualityReportTemplateDto>> GetDefaultTemplatesAsync();

        /// <summary>
        /// 根据模板生成报表
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="parameters">报表参数</param>
        /// <returns>生成的报表内容</returns>
        Task<string> GenerateReportAsync(Guid templateId, Dictionary<string, string> parameters);
    }
}