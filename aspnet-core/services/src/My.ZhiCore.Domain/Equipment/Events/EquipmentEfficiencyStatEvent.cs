using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备效率统计事件
    /// </summary>
    public class EquipmentEfficiencyStatEvent : EquipmentEventData
    {
        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; }

        /// <summary>
        /// 设备运行时间（分钟）
        /// </summary>
        public decimal RunningMinutes { get; }

        /// <summary>
        /// 设备停机时间（分钟）
        /// </summary>
        public decimal DowntimeMinutes { get; }

        /// <summary>
        /// 设备故障时间（分钟）
        /// </summary>
        public decimal FaultMinutes { get; }

        /// <summary>
        /// 设备维护时间（分钟）
        /// </summary>
        public decimal MaintenanceMinutes { get; }

        /// <summary>
        /// 设备空闲时间（分钟）
        /// </summary>
        public decimal IdleMinutes { get; }

        /// <summary>
        /// 设备利用率（%）
        /// </summary>
        public decimal UtilizationRate { get; }

        /// <summary>
        /// 设备可用率（%）
        /// </summary>
        public decimal AvailabilityRate { get; }

        /// <summary>
        /// 设备性能效率（%）
        /// </summary>
        public decimal PerformanceRate { get; }

        public EquipmentEfficiencyStatEvent(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            DateTime startTime,
            DateTime endTime,
            decimal runningMinutes,
            decimal downtimeMinutes,
            decimal faultMinutes,
            decimal maintenanceMinutes,
            decimal idleMinutes,
            decimal utilizationRate,
            decimal availabilityRate,
            decimal performanceRate,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null)
            : base(equipmentId, equipmentCode, equipmentName, operatorId, operatorName, reason, remark)
        {
            StartTime = startTime;
            EndTime = endTime;
            RunningMinutes = runningMinutes;
            DowntimeMinutes = downtimeMinutes;
            FaultMinutes = faultMinutes;
            MaintenanceMinutes = maintenanceMinutes;
            IdleMinutes = idleMinutes;
            UtilizationRate = utilizationRate;
            AvailabilityRate = availabilityRate;
            PerformanceRate = performanceRate;
        }
    }
}