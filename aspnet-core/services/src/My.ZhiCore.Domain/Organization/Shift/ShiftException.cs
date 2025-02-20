using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次异常记录实体 - 用于记录班次中出现的异常情况
    /// </summary>
    public class ShiftException : Entity<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; private set; }

        /// <summary>
        /// 异常发生时间
        /// </summary>
        public DateTime OccurredTime { get; private set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; private set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 影响程度
        /// </summary>
        public string Severity { get; private set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string Resolution { get; private set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        public Guid RecordedByEmployeeId { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ShiftException() { }

        public ShiftException(
            Guid id,
            Guid shiftId,
            DateTime occurredTime,
            string exceptionType,
            string description,
            string severity,
            string status,
            string resolution,
            Guid recordedByEmployeeId,
            string remarks = null)
        {
            Id = id;
            ShiftId = shiftId;
            OccurredTime = occurredTime;
            ExceptionType = exceptionType;
            Description = description;
            Severity = severity;
            Status = status;
            Resolution = resolution;
            RecordedByEmployeeId = recordedByEmployeeId;
            Remarks = remarks;
        }
    }
}