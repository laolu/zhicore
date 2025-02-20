using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备使用记录实体类，用于跟踪和记录设备的使用情况
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录设备使用的基本信息（操作人、时间等）
    /// - 跟踪设备使用的开始和结束时间
    /// - 计算设备使用时长
    /// - 记录使用目的和备注信息
    /// </remarks>
    public class EquipmentUsageRecord : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; private set; }
    
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; private set; }
    
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; private set; }
    
        /// <summary>
        /// 使用开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }
    
        /// <summary>
        /// 使用结束时间，为null表示尚未结束
        /// </summary>
        public DateTime? EndTime { get; private set; }
    
        /// <summary>
        /// 使用目的
        /// </summary>
        public string Purpose { get; private set; }
    
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Notes { get; private set; }
    
        /// <summary>
        /// 使用时长，在使用结束后计算
        /// </summary>
        public TimeSpan? Duration { get; private set; }
    
        /// <summary>
        /// 受保护的构造函数，用于ORM
        /// </summary>
        protected EquipmentUsageRecord()
        {
        }
    
        /// <summary>
        /// 创建新的设备使用记录
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="startTime">使用开始时间</param>
        /// <param name="purpose">使用目的</param>
        /// <param name="notes">备注信息（可选）</param>
        public EquipmentUsageRecord(
            Guid id,
            Guid equipmentId,
            string operatorId,
            string operatorName,
            DateTime startTime,
            string purpose,
            string notes = null) : base(id)
        {
            EquipmentId = equipmentId;
            OperatorId = operatorId;
            OperatorName = operatorName;
            StartTime = startTime;
            Purpose = purpose;
            Notes = notes;
        }
    
        /// <summary>
        /// 完成设备使用记录
        /// </summary>
        /// <param name="endTime">使用结束时间</param>
        /// <exception cref="ArgumentException">当结束时间早于或等于开始时间时抛出</exception>
        public void CompleteUsage(DateTime endTime)
        {
            if (endTime <= StartTime)
                throw new ArgumentException("End time must be later than start time.", nameof(endTime));
    
            EndTime = endTime;
            Duration = endTime - StartTime;
        }
    
        /// <summary>
        /// 更新备注信息
        /// </summary>
        /// <param name="notes">新的备注信息</param>
        public void UpdateNotes(string notes)
        {
            Notes = notes;
        }
    }
}