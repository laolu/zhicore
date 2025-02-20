using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行记录实体
    /// </summary>
    public class OperationExecution : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid ProcessOperationId { get; private set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? EndTime { get; private set; }

        /// <summary>
        /// 实际执行时长（分钟）
        /// </summary>
        public int? ActualDuration { get; private set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public OperationExecutionStatus Status { get; private set; }

        /// <summary>
        /// 执行结果
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        /// <summary>
        /// 生产执行记录ID
        /// </summary>
        public Guid? ProductionExecutionId { get; private set; }

        protected OperationExecution() { }

        public OperationExecution(
            Guid id,
            Guid operationId)
        {
            Id = id;
            OperationId = operationId;
            StartTime = DateTime.Now;
            Status = OperationExecutionStatus.InProgress;
            ProductionExecutionId = null;
        }

        public void Complete(string result = null, string remarks = null)
        {
            EndTime = DateTime.Now;
            ActualDuration = (int)(EndTime.Value - StartTime).TotalMinutes;
            Status = OperationExecutionStatus.Completed;
            Result = result;
            Remarks = remarks;
        }

        public void Fail(string result, string remarks = null)
        {
            EndTime = DateTime.Now;
            ActualDuration = (int)(EndTime.Value - StartTime).TotalMinutes;
            Status = OperationExecutionStatus.Failed;
            Result = result;
            Remarks = remarks;
        }
    }
}