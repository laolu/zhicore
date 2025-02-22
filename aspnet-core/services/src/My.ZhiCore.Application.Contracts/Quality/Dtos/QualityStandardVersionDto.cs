using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Quality.Dtos
{
    /// <summary>
    /// 质量标准版本DTO
    /// </summary>
    public class QualityStandardVersionDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 标准ID
        /// </summary>
        public Guid StandardId { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 变更说明
        /// </summary>
        public string ChangeDescription { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public QualityStandardApprovalStatus ApprovalStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string ApprovalComment { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? ApproverId { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }
    }

    /// <summary>
    /// 质量标准审核状态
    /// </summary>
    public enum QualityStandardApprovalStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 已通过
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 已驳回
        /// </summary>
        Rejected = 2
    }

    /// <summary>
    /// 创建质量标准版本DTO
    /// </summary>
    public class CreateQualityStandardVersionDto
    {
        /// <summary>
        /// 标准ID
        /// </summary>
        public Guid StandardId { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 变更说明
        /// </summary>
        public string ChangeDescription { get; set; }
    }

    /// <summary>
    /// 审核质量标准版本DTO
    /// </summary>
    public class ApproveQualityStandardVersionDto
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string Comment { get; set; }
    }
}