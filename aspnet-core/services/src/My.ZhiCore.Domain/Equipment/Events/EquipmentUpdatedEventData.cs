using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备更新事件
    /// </summary>
    public class EquipmentUpdatedEventData : EquipmentEventData
    {
        public EquipmentUpdatedEventData(Guid equipmentId, string equipmentCode, string equipmentName)
            : base(equipmentId, equipmentCode, equipmentName)
        {
        }
    }
}