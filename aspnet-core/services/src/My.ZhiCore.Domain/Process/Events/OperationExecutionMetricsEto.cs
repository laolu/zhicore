using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行监控指标事件
    /// </summary>
    public class OperationExecutionMetricsEto
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 执行ID
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// 指标采集时间
        /// </summary>
        public DateTime CollectionTime { get; set; }

        /// <summary>
        /// 设备运行时间（分钟）
        /// </summary>
        public decimal EquipmentRuntime { get; set; }

        /// <summary>
        /// 设备停机时间（分钟）
        /// </summary>
        public decimal EquipmentDowntime { get; set; }

        /// <summary>
        /// 产品合格率（百分比）
        /// </summary>
        public decimal QualityRate { get; set; }

        /// <summary>
        /// 生产效率（实际产出/计划产出）
        /// </summary>
        public decimal ProductionEfficiency { get; set; }

        /// <summary>
        /// 能源利用率（百分比）
        /// </summary>
        public decimal EnergyEfficiency { get; set; }

        /// <summary>
        /// 设备综合效率（OEE）
        /// </summary>
        public decimal OverallEquipmentEffectiveness { get; set; }

        /// <summary>
        /// 指标状态（正常/异常）
        /// </summary>
        public string MetricsStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}