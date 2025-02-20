using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序资源使用记录实体
    /// </summary>
    public class OperationResourceExecution : Entity<Guid>
    {
        /// <summary>
        /// 关联的工序资源需求Id
        /// </summary>
        public Guid ProcessStepResourceId { get; private set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime? EndTime { get; private set; }

        /// <summary>
        /// 实际使用数量
        /// </summary>
        public int ActualQuantity { get; private set; }

        /// <summary>
        /// 资源使用效率（百分比）
        /// </summary>
        public decimal? Efficiency { get; private set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public ResourceExecutionStatus Status { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected OperationResourceExecution()
        {
        }

        public OperationResourceExecution(
            Guid id,
            Guid processStepResourceId,
            DateTime startTime,
            int actualQuantity)
        {
            Id = id;
            ProcessStepResourceId = processStepResourceId;
            SetStartTime(startTime);
            SetActualQuantity(actualQuantity);
            Status = ResourceExecutionStatus.InProgress;
        }

        private void SetStartTime(DateTime startTime)
        {
            if (startTime > DateTime.Now)
            {
                throw new ArgumentException("开始时间不能晚于当前时间", nameof(startTime));
            }

            StartTime = startTime;
        }

        private void SetActualQuantity(int actualQuantity)
        {
            if (actualQuantity <= 0)
            {
                throw new ArgumentException("实际使用数量必须大于0", nameof(actualQuantity));
            }

            ActualQuantity = actualQuantity;
        }

        private void SetEndTime(DateTime endTime)
        {
            if (endTime <= StartTime)
            {
                throw new ArgumentException("结束时间必须晚于开始时间", nameof(endTime));
            }

            EndTime = endTime;
        }

        private void SetEfficiency(decimal efficiency)
        {
            if (efficiency < 0 || efficiency > 100)
            {
                throw new ArgumentException("使用效率必须在0-100之间", nameof(efficiency));
            }

            Efficiency = efficiency;
        }

        private void SetRemarks(string remarks)
        {
            if (!string.IsNullOrEmpty(remarks) && remarks.Length > 500)
            {
                throw new ArgumentException("备注长度不能超过500个字符", nameof(remarks));
            }

            Remarks = remarks;
        }

        /// <summary>
        /// 完成资源使用
        /// </summary>
        public void Complete(DateTime endTime, decimal efficiency, string remarks = null)
        {
            if (Status != ResourceExecutionStatus.InProgress)
            {
                throw new InvalidOperationException("只有进行中的资源使用记录才能标记为完成");
            }

            SetEndTime(endTime);
            SetEfficiency(efficiency);
            SetRemarks(remarks);
            Status = ResourceExecutionStatus.Completed;
        }

        /// <summary>
        /// 中止资源使用
        /// </summary>
        public void Abort(DateTime endTime, string remarks)
        {
            if (Status != ResourceExecutionStatus.InProgress)
            {
                throw new InvalidOperationException("只有进行中的资源使用记录才能中止");
            }

            SetEndTime(endTime);
            SetRemarks(remarks);
            Status = ResourceExecutionStatus.Aborted;
        }
    }
}