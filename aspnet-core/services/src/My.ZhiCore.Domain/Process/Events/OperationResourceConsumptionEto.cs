using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源消耗事件
    /// </summary>
    public class OperationResourceConsumptionEto
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
        /// 计划消耗数量
        /// </summary>
        public decimal PlannedConsumption { get; set; }

        /// <summary>
        /// 实际消耗数量
        /// </summary>
        public decimal ActualConsumption { get; set; }

        /// <summary>
        /// 消耗单位
        /// </summary>
        public string ConsumptionUnit { get; set; }

        /// <summary>
        /// 消耗开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 消耗结束时间
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