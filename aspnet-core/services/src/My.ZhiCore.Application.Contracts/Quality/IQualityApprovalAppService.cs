using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量审核服务接口
    /// </summary>
    public interface IQualityApprovalAppService : IApplicationService
    {
        /// <summary>
        /// 获取质量审核列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>质量审核列表</returns>
        Task<PagedResultDto<QualityApprovalDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取质量审核详情
        /// </summary>
        /// <param name="id">审核ID</param>
        /// <returns>质量审核详情</returns>
        Task<QualityApprovalDto> GetAsync(Guid id);

        /// <summary>
        /// 创建质量审核
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的质量审核</returns>
        Task<QualityApprovalDto> CreateAsync(CreateQualityApprovalDto input);

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="id">审核ID</param>
        /// <returns>更新后的质量审核</returns>
        Task<QualityApprovalDto> SubmitAsync(Guid id);

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id">审核ID</param>
        /// <param name="input">审核参数</param>
        /// <returns>更新后的质量审核</returns>
        Task<QualityApprovalDto> ApproveAsync(Guid id, ApproveQualityDto input);

        /// <summary>
        /// 审核驳回
        /// </summary>
        /// <param name="id">审核ID</param>
        /// <param name="input">驳回参数</param>
        /// <returns>更新后的质量审核</returns>
        Task<QualityApprovalDto> RejectAsync(Guid id, RejectQualityDto input);

        /// <summary>
        /// 获取待审核列表
        /// </summary>
        /// <returns>待审核列表</returns>
        Task<List<QualityApprovalDto>> GetPendingListAsync();
    }

    /// <summary>
    /// 质量审核DTO
    /// </summary>
    public class QualityApprovalDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 审核编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 审核标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 审核类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 关联对象ID
        /// </summary>
        public Guid RelatedId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public QualityApprovalStatus Status { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? ApproverId { get; set; }
    }

    /// <summary>
    /// 创建质量审核DTO
    /// </summary>
    public class CreateQualityApprovalDto
    {
        /// <summary>
        /// 审核标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 审核类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 关联对象ID
        /// </summary>
        public Guid RelatedId { get; set; }
    }

    /// <summary>
    /// 审核通过DTO
    /// </summary>
    public class ApproveQualityDto
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comment { get; set; }
    }

    /// <summary>
    /// 审核驳回DTO
    /// </summary>
    public class RejectQualityDto
    {
        /// <summary>
        /// 驳回原因
        /// </summary>
        public string Reason { get; set; }
    }

    /// <summary>
    /// 质量审核状态
    /// </summary>
    public enum QualityApprovalStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 已通过
        /// </summary>
        Approved = 2,

        /// <summary>
        /// 已驳回
        /// </summary>
        Rejected = 3
    }
}