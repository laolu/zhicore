using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备维修事件
    /// </summary>
    public class EquipmentMaintenanceEventData : EquipmentEventData
    {
        /// <summary>
        /// 维修类型
        /// </summary>
        public string MaintenanceType { get; }

        /// <summary>
        /// 维修描述
        /// </summary>
        public string MaintenanceDescription { get; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string MaintenanceStaff { get; }

        /// <summary>
        /// 维修开始时间
        /// </summary>
        public DateTime StartTime { get; }

        /// <summary>
        /// 维修结束时间
        /// </summary>
        public DateTime? EndTime { get; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public string Status { get; }

        public EquipmentMaintenanceEventData(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            string maintenanceType,
            string maintenanceDescription,
            string maintenanceStaff,
            DateTime startTime,
            DateTime? endTime,
            string status)
            : base(equipmentId, equipmentCode, equipmentName)
        {
            MaintenanceType = maintenanceType;
            MaintenanceDescription = maintenanceDescription;
            MaintenanceStaff = maintenanceStaff;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }
    }
}