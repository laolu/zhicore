using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序实体
    /// </summary>
    public class Operation : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工序编号
        /// </summary>
        public string OperationNumber { get; private set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 标准工时（分钟）
        /// </summary>
        public int StandardTime { get; private set; }

        /// <summary>
        /// 所属工单ID
        /// </summary>
        public Guid WorkOrderId { get; private set; }

        /// <summary>
        /// 工序顺序号
        /// </summary>
        public int SequenceNumber { get; private set; }

        /// <summary>
        /// 工序状态
        /// </summary>
        public OperationStatus Status { get; private set; }

        protected Operation() { }

        public Operation(
            Guid id,
            string operationNumber,
            string name,
            Guid workOrderId,
            int sequenceNumber,
            int standardTime,
            string description = null)
        {
            Id = id;
            OperationNumber = operationNumber;
            Name = name;
            WorkOrderId = workOrderId;
            SequenceNumber = sequenceNumber;
            StandardTime = standardTime;
            Description = description;
            Status = OperationStatus.Pending;
        }

        public void Start()
        {
            if (Status != OperationStatus.Pending)
                throw new InvalidOperationException("只有待执行的工序才能开始");

            Status = OperationStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != OperationStatus.InProgress)
                throw new InvalidOperationException("只有进行中的工序才能完成");

            Status = OperationStatus.Completed;
        }

        public void Pause()
        {
            if (Status != OperationStatus.InProgress)
                throw new InvalidOperationException("只有进行中的工序才能暂停");

            Status = OperationStatus.Paused;
        }

        public void Resume()
        {
            if (Status != OperationStatus.Paused)
                throw new InvalidOperationException("只有暂停的工序才能恢复");

            Status = OperationStatus.InProgress;
        }
    }
}