using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量问题DTO
    /// </summary>
    public class QualityIssueDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 问题编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 问题等级
        /// </summary>
        public QualityIssueSeverity Severity { get; set; }

        /// <summary>
        /// 问题状态
        /// </summary>
        public QualityIssueStatus Status { get; set; }

        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime DiscoveryTime { get; set; }

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? ResolvedTime { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        public string Solution { get; set; }

        /// <summary>
        /// 相关质量检验ID
        /// </summary>
        public Guid? InspectionId { get; set; }
    }

    /// <summary>
    /// 创建质量问题DTO
    /// </summary>
    public class CreateQualityIssueDto
    {
        /// <summary>
        /// 问题编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 问题等级
        /// </summary>
        public QualityIssueSeverity Severity { get; set; }

        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime DiscoveryTime { get; set; }

        /// <summary>
        /// 相关质量检验ID
        /// </summary>
        public Guid? InspectionId { get; set; }
    }

    /// <summary>
    /// 更新质量问题DTO
    /// </summary>
    public class UpdateQualityIssueDto
    {
        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 问题类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 问题等级
        /// </summary>
        public QualityIssueSeverity Severity { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        public string Solution { get; set; }
    }

    /// <summary>
    /// 质量问题等级
    /// </summary>
    public enum QualityIssueSeverity
    {
        /// <summary>
        /// 低
        /// </summary>
        Low = 0,

        /// <summary>
        /// 中
        /// </summary>
        Medium = 1,

        /// <summary>
        /// 高
        /// </summary>
        High = 2,

        /// <summary>
        /// 严重
        /// </summary>
        Critical = 3
    }

    /// <summary>
    /// 质量问题状态
    /// </summary>
    public enum QualityIssueStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Open = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已解决
        /// </summary>
        Resolved = 2,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 3
    }
}