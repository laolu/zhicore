using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备删除事件
    /// </summary>
    public class EquipmentDeletedEventData : EquipmentEventData
    {
        public EquipmentDeletedEventData(Guid equipmentId, string equipmentCode, string equipmentName)
            : base(equipmentId, equipmentCode, equipmentName)
        {
        }
    }
}