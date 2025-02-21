using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备创建事件
    /// </summary>
    public class EquipmentCreatedEventData : EquipmentEventData
    {
        public EquipmentCreatedEventData(Guid equipmentId, string equipmentCode, string equipmentName)
            : base(equipmentId, equipmentCode, equipmentName)
        {
        }
    }
}