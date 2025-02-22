using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Workshop.Dtos
{
    /// <summary>
    /// 工作中心状态DTO
    /// </summary>
    public class WorkCenterStatusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 工作中心ID
        /// </summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>
        /// 状态编码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 持续时间（分钟）
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 是否为当前状态
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 关联设备ID
        /// </summary>
        public Guid? DeviceId { get; set; }
    }

    /// <summary>
    /// 工作中心状态变更DTO
    /// </summary>
    public class ChangeWorkCenterStatusDto
    {
        /// <summary>
        /// 新状态编码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 关联设备ID
        /// </summary>
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 工作中心状态历史查询DTO
    /// </summary>
    public class GetWorkCenterStatusHistoryDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 工作中心ID
        /// </summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 状态编码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 关联设备ID
        /// </summary>
        public Guid? DeviceId { get; set; }
    }
}