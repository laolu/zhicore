using System;

namespace My.ZhiCore.Equipment.Dtos
{
    /// <summary>
    /// 设备运行状态分析DTO
    /// </summary>
    public class EquipmentOperationAnalysisDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 运行时间（小时）
        /// </summary>
        public decimal OperationHours { get; set; }

        /// <summary>
        /// 停机时间（小时）
        /// </summary>
        public decimal DowntimeHours { get; set; }

        /// <summary>
        /// 设备利用率
        /// </summary>
        public decimal UtilizationRate { get; set; }

        /// <summary>
        /// 运行效率
        /// </summary>
        public decimal OperationalEfficiency { get; set; }
    }

    /// <summary>
    /// 设备性能趋势DTO
    /// </summary>
    public class EquipmentPerformanceTrendDto
    {
        /// <summary>
        /// 时间点
        /// </summary>
        public DateTime[] TimePoints { get; set; }

        /// <summary>
        /// 性能指标值
        /// </summary>
        public decimal[] PerformanceValues { get; set; }

        /// <summary>
        /// 指标类型
        /// </summary>
        public string[] IndicatorTypes { get; set; }
    }

    /// <summary>
    /// 设备故障分析DTO
    /// </summary>
    public class EquipmentFailureAnalysisDto
    {
        /// <summary>
        /// 故障类型
        /// </summary>
        public string[] FailureTypes { get; set; }

        /// <summary>
        /// 故障次数
        /// </summary>
        public int[] FailureCounts { get; set; }

        /// <summary>
        /// 平均修复时间（小时）
        /// </summary>
        public decimal[] MeanTimeToRepair { get; set; }

        /// <summary>
        /// 故障影响程度
        /// </summary>
        public string[] SeverityLevels { get; set; }
    }
}