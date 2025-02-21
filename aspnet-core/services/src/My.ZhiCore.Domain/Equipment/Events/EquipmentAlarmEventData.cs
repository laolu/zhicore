using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备报警事件
    /// </summary>
    public class EquipmentAlarmEventData : EquipmentEventData
    {
        /// <summary>
        /// 报警类型
        /// </summary>
        public string AlarmType { get; }

        /// <summary>
        /// 报警等级
        /// </summary>
        public string AlarmLevel { get; }

        /// <summary>
        /// 报警描述
        /// </summary>
        public string AlarmDescription { get; }

        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; }

        /// <summary>
        /// 报警状态
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// 处理人员
        /// </summary>
        public string Handler { get; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime { get; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandleResult { get; }

        public EquipmentAlarmEventData(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            string alarmType,
            string alarmLevel,
            string alarmDescription,
            string alarmValue,
            string status,
            string handler = null,
            DateTime? handleTime = null,
            string handleResult = null)
            : base(equipmentId, equipmentCode, equipmentName)
        {
            AlarmType = alarmType;
            AlarmLevel = alarmLevel;
            AlarmDescription = alarmDescription;
            AlarmValue = alarmValue;
            Status = status;
            Handler = handler;
            HandleTime = handleTime;
            HandleResult = handleResult;
        }
    }
}