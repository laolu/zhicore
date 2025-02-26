using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验DTO
    /// </summary>
    public class QualityInspectionDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 检验编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 检验名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 检验描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 检验状态
        /// </summary>
        public QualityInspectionStatus Status { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public QualityInspectionResult Result { get; set; }

        /// <summary>
        /// 检验项目列表
        /// </summary>
        public List<QualityInspectionItemDto> Items { get; set; }
    }

    /// <summary>
    /// 创建质量检验DTO
    /// </summary>
    public class CreateQualityInspectionDto
    {
        /// <summary>
        /// 检验编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 检验名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 检验描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// 更新质量检验DTO
    /// </summary>
    public class UpdateQualityInspectionDto
    {
        /// <summary>
        /// 检验名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 检验描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// 质量检验状态
    /// </summary>
    public enum QualityInspectionStatus
    {
        /// <summary>
        /// 待检验
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 检验中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 3
    }

    /// <summary>
    /// 质量检验结果
    /// </summary>
    public enum QualityInspectionResult
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 合格
        /// </summary>
        Qualified = 1,

        /// <summary>
        /// 不合格
        /// </summary>
        Unqualified = 2
    }
}