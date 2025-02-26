using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源利用率事件
    /// </summary>
    public class OperationResourceUtilizationEto
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 利用率（百分比）
        /// </summary>
        public decimal UtilizationRate { get; set; }

        /// <summary>
        /// 计划利用时长（分钟）
        /// </summary>
        public decimal PlannedDuration { get; set; }

        /// <summary>
        /// 实际利用时长（分钟）
        /// </summary>
        public decimal ActualDuration { get; set; }

        /// <summary>
        /// 空闲时长（分钟）
        /// </summary>
        public decimal IdleDuration { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}