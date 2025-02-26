using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源效率事件
    /// </summary>
    public class OperationResourceEfficiencyEto
    {
        /// <summary>
        /// 工序执行ID
        /// </summary>
        public Guid ExecutionId { get; set; }

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
        /// 资源利用率（%）
        /// </summary>
        public decimal UtilizationRate { get; set; }

        /// <summary>
        /// 资源产出率（件/小时）
        /// </summary>
        public decimal OutputRate { get; set; }

        /// <summary>
        /// 资源损耗率（%）
        /// </summary>
        public decimal WastageRate { get; set; }

        /// <summary>
        /// 资源故障率（%）
        /// </summary>
        public decimal FailureRate { get; set; }

        /// <summary>
        /// 资源能耗（kW·h）
        /// </summary>
        public decimal EnergyConsumption { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}