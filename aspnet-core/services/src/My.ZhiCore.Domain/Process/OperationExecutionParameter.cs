using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行参数记录实体
    /// </summary>
    public class OperationExecutionParameter : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工序执行记录ID
        /// </summary>
        public Guid OperationExecutionId { get; private set; }

        /// <summary>
        /// 工序参数ID
        /// </summary>
        public Guid ProcessStepParameterId { get; private set; }

        /// <summary>
        /// 参数实际值
        /// </summary>
        public string ActualValue { get; private set; }

        /// <summary>
        /// 是否在允许范围内
        /// </summary>
        public bool IsWithinRange { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected OperationExecutionParameter() { }

        public OperationExecutionParameter(
            Guid id,
            Guid operationExecutionId,
            Guid processStepParameterId,
            string actualValue,
            bool isWithinRange,
            string remarks = null)
        {
            Id = id;
            OperationExecutionId = operationExecutionId;
            ProcessStepParameterId = processStepParameterId;
            ActualValue = actualValue;
            IsWithinRange = isWithinRange;
            Remarks = remarks;
        }

        public void UpdateValue(string actualValue, bool isWithinRange, string remarks = null)
        {
            ActualValue = actualValue;
            IsWithinRange = isWithinRange;
            Remarks = remarks;
        }
    }
}