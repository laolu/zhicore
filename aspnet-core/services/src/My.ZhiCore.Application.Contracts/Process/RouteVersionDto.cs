using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线版本DTO
    /// </summary>
    public class RouteVersionDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 版本状态
        /// </summary>
        public RouteVersionStatus Status { get; set; }

        /// <summary>
        /// 所属工艺路线ID
        /// </summary>
        public Guid RouteId { get; set; }

        /// <summary>
        /// 工艺步骤列表
        /// </summary>
        public List<RouteStepDto> Steps { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? ReviewerId { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ReviewTime { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string ReviewComment { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string ChangeContent { get; set; }
    }

    /// <summary>
    /// 创建工艺路线版本DTO
    /// </summary>
    public class CreateRouteVersionDto
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 初始状态
        /// </summary>
        public RouteVersionStatus InitialStatus { get; set; } = RouteVersionStatus.Draft;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string ChangeContent { get; set; }
    }

    /// <summary>
    /// 更新工艺路线版本DTO
    /// </summary>
    public class UpdateRouteVersionDto
    {
        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 版本状态
        /// </summary>
        public RouteVersionStatus Status { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string ReviewComment { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string ChangeContent { get; set; }
    }

    /// <summary>
    /// 工艺路线版本状态
    /// </summary>
    public enum RouteVersionStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        PendingReview = 1,

        /// <summary>
        /// 已审核
        /// </summary>
        Reviewed = 2,

        /// <summary>
        /// 已发布
        /// </summary>
        Published = 3,

        /// <summary>
        /// 已作废
        /// </summary>
        Deprecated = 4
    }
}