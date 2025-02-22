using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Events
{
    /// <summary>
    /// 设备利用率变更事件
    /// </summary>
    [EventName("My.ZhiCore.Production.EquipmentUtilizationChanged")]
    public class EquipmentUtilizationChangedEto
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 设备利用率
        /// </summary>
        public decimal UtilizationRate { get; set; }

        /// <summary>
        /// 运行时间（小时）
        /// </summary>
        public decimal RunningHours { get; set; }

        /// <summary>
        /// 停机时间（小时）
        /// </summary>
        public decimal DowntimeHours { get; set; }

        /// <summary>
        /// 维护时间（小时）
        /// </summary>
        public decimal MaintenanceHours { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}