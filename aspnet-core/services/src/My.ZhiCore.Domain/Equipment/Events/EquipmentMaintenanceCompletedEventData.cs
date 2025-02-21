using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备维护完成事件
    /// </summary>
    public class EquipmentMaintenanceCompletedEventData : EquipmentEventData
    {
        /// <summary>
        /// 维护完成日期
        /// </summary>
        public DateTime MaintenanceDate { get; }

        /// <summary>
        /// 下次维护日期
        /// </summary>
        public DateTime? NextMaintenanceDate { get; }

        /// <summary>
        /// 维护成本
        /// </summary>
        public decimal MaintenanceCost { get; }

        public EquipmentMaintenanceCompletedEventData(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            DateTime maintenanceDate,
            DateTime? nextMaintenanceDate,
            decimal maintenanceCost) : base(equipmentId, equipmentCode, equipmentName)
        {
            MaintenanceDate = maintenanceDate;
            NextMaintenanceDate = nextMaintenanceDate;
            MaintenanceCost = maintenanceCost;
        }
    }
}