using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备运行状态变更事件
    /// </summary>
    public class EquipmentStatusChangeEventData : EquipmentEventData
    {
        /// <summary>
        /// 原状态
        /// </summary>
        public string OldStatus { get; }

        /// <summary>
        /// 新状态
        /// </summary>
        public string NewStatus { get; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; }

        public EquipmentStatusChangeEventData(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            string oldStatus,
            string newStatus,
            string changeReason,
            string @operator,
            string remarks = null)
            : base(equipmentId, equipmentCode, equipmentName)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangeReason = changeReason;
            Operator = @operator;
            Remarks = remarks;
        }
    }
}