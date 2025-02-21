using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备状态变更事件
    /// </summary>
    public class EquipmentStatusChangedEventData : EquipmentEventData
    {
        /// <summary>
        /// 原状态
        /// </summary>
        public EquipmentStatus OldStatus { get; }

        /// <summary>
        /// 新状态
        /// </summary>
        public EquipmentStatus NewStatus { get; }

        /// <summary>
        /// 状态变更原因
        /// </summary>
        public string Reason { get; }

        public EquipmentStatusChangedEventData(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            EquipmentStatus oldStatus,
            EquipmentStatus newStatus,
            string reason = null) : base(equipmentId, equipmentCode, equipmentName)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Reason = reason;
        }
    }
}