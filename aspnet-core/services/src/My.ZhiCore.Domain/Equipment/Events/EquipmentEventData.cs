using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备事件基类
    /// </summary>
    [EventName("My.ZhiCore.Equipment")]
    public class EquipmentEventData : IEventDataWithInheritableGenericArguments
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime OccurredTime { get; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid? OperatorId { get; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperatorName { get; }

        /// <summary>
        /// 操作原因
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; }

        public EquipmentEventData(Guid equipmentId, string equipmentCode, string equipmentName, 
            Guid? operatorId = null, string operatorName = null, string reason = null, string remark = null)
        {
            EquipmentId = equipmentId;
            EquipmentCode = equipmentCode;
            EquipmentName = equipmentName;
            OccurredTime = DateTime.Now;
            OperatorId = operatorId;
            OperatorName = operatorName;
            Reason = reason;
            Remark = remark;
        }
    }
}