using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量标准服务接口
    /// </summary>
    public interface IQualityStandardAppService : IApplicationService
    {
        /// <summary>
        /// 获取质量标准列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>质量标准列表</returns>
        Task<PagedResultDto<QualityStandardDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取质量标准详情
        /// </summary>
        /// <param name="id">质量标准ID</param>
        /// <returns>质量标准详情</returns>
        Task<QualityStandardDto> GetAsync(Guid id);

        /// <summary>
        /// 创建质量标准
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的质量标准</returns>
        Task<QualityStandardDto> CreateAsync(CreateQualityStandardDto input);

        /// <summary>
        /// 更新质量标准
        /// </summary>
        /// <param name="id">质量标准ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的质量标准</returns>
        Task<QualityStandardDto> UpdateAsync(Guid id, UpdateQualityStandardDto input);

        /// <summary>
        /// 删除质量标准
        /// </summary>
        /// <param name="id">质量标准ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取适用的质量标准列表
        /// </summary>
        /// <param name="productType">产品类型</param>
        /// <returns>适用的质量标准列表</returns>
        Task<List<QualityStandardDto>> GetApplicableStandardsAsync(string productType);

        /// <summary>
        /// 获取质量标准版本列表
        /// </summary>
        /// <param name="standardId">标准ID</param>
        /// <returns>标准版本列表</returns>
        Task<List<QualityStandardVersionDto>> GetVersionsAsync(Guid standardId);

        /// <summary>
        /// 创建质量标准版本
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的标准版本</returns>
        Task<QualityStandardVersionDto> CreateVersionAsync(CreateQualityStandardVersionDto input);

        /// <summary>
        /// 审核质量标准版本
        /// </summary>
        /// <param name="id">版本ID</param>
        /// <param name="input">审核参数</param>
        /// <returns>审核后的标准版本</returns>
        Task<QualityStandardVersionDto> ApproveVersionAsync(Guid id, ApproveQualityStandardVersionDto input);
    }

    /// <summary>
    /// 质量标准DTO
    /// </summary>
    public class QualityStandardDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 标准编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标准描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 标准类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 适用产品类型
        /// </summary>
        public string[] ApplicableProductTypes { get; set; }

        /// <summary>
        /// 标准参数
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 创建质量标准DTO
    /// </summary>
    public class CreateQualityStandardDto
    {
        /// <summary>
        /// 标准编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标准描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 标准类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 适用产品类型
        /// </summary>
        public string[] ApplicableProductTypes { get; set; }

        /// <summary>
        /// 标准参数
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 更新质量标准DTO
    /// </summary>
    public class UpdateQualityStandardDto
    {
        /// <summary>
        /// 标准名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标准描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 标准类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 适用产品类型
        /// </summary>
        public string[] ApplicableProductTypes { get; set; }

        /// <summary>
        /// 标准参数
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}