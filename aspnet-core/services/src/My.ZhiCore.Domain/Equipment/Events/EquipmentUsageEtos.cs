using System;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备使用开始事件数据传输对象
    /// </summary>
    public class EquipmentUsageStartedEto
    {
        /// <summary>
        /// 使用记录ID
        /// </summary>
        public Guid UsageRecordId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 使用开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }

    /// <summary>
    /// 设备使用结束事件数据传输对象
    /// </summary>
    public class EquipmentUsageCompletedEto
    {
        /// <summary>
        /// 使用记录ID
        /// </summary>
        public Guid UsageRecordId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 使用结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 使用时长
        /// </summary>
        public TimeSpan Duration { get; set; }
    }
}