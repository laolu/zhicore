using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Workshop.Dtos
{
    /// <summary>
    /// 车间状态DTO
    /// </summary>
    public class WorkshopStatusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

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
    }

    /// <summary>
    /// 车间状态变更DTO
    /// </summary>
    public class ChangeWorkshopStatusDto
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
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 车间状态历史查询DTO
    /// </summary>
    public class GetWorkshopStatusHistoryDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

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
    }
}