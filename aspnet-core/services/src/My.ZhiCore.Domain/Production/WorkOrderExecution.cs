using System;
using Volo.Abp.Domain.Entities.Auditing;
using My.ZhiCore.Production.Events;

namespace My.ZhiCore.Production.Execution
{
    /// <summary>
    /// 生产执行记录实体 - 用于记录和管理生产过程中的具体执行情况
    /// </summary>
    /// <remarks>
    /// 该实体负责管理生产执行的全生命周期，包含以下核心功能：
    /// - 记录生产执行的基本信息（执行编号、工位、操作人等）
    /// - 跟踪生产数量（合格品和不合格品）
    /// - 管理执行状态（进行中、已完成、已取消）
    /// - 生成生产完成事件用于后续处理
    /// </remarks>
    public class ProductionExecution : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 执行编号
        /// </summary>
        public string ExecutionNumber { get; private set; }

        /// <summary>
        /// 工单Id
        /// </summary>
        public Guid WorkOrderId { get; private set; }

        /// <summary>
        /// 工位编号
        /// </summary>
        public string WorkstationCode { get; private set; }

        /// <summary>
        /// 操作人员编号
        /// </summary>
        public string OperatorCode { get; private set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int QualifiedQuantity { get; private set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int DefectQuantity { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ProductionExecutionStatus Status { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        /// <summary>
        /// 工序执行ID
        /// </summary>
        public Guid? OperationExecutionId { get; private set; }

        protected ProductionExecution() { }

        /// <summary>
        /// 创建新的生产执行记录
        /// </summary>
        /// <param name="id">执行记录唯一标识</param>
        /// <param name="executionNumber">执行编号</param>
        /// <param name="productionTaskId">关联的工单ID</param>
        /// <param name="workstationCode">工位编号</param>
        /// <param name="operatorCode">操作人员编号</param>
        /// <param name="startTime">开始时间</param>
        /// <remarks>
        /// 创建新的生产执行记录实例，初始状态为进行中，数量相关字段初始化为0
        /// </remarks>
        public ProductionExecution(
            Guid id,
            string executionNumber,
            Guid productionTaskId,
            string workstationCode,
            string operatorCode,
            DateTime startTime)
        {
            Id = id;
            ExecutionNumber = executionNumber;
            WorkOrderId = productionTaskId;
            WorkstationCode = workstationCode;
            OperatorCode = operatorCode;
            StartTime = startTime;
            OperationExecutionId = null;
            Status = ProductionExecutionStatus.InProgress;
            Quantity = 0;
            QualifiedQuantity = 0;
            DefectQuantity = 0;
        }

        /// <summary>
        /// 更新生产数量
        /// </summary>
        /// <param name="qualifiedQuantity">合格品数量</param>
        /// <param name="defectQuantity">不合格品数量</param>
        /// <remarks>
        /// 更新生产执行记录的数量信息，包括：
        /// - 合格品数量
        /// - 不合格品数量
        /// - 总生产数量（自动计算）
        /// </remarks>
        /// <exception cref="InvalidOperationException">当执行记录不处于进行中状态时抛出</exception>
        /// <exception cref="ArgumentException">当输入的数量为负数时抛出</exception>
        public void UpdateQuantity(int qualifiedQuantity, int defectQuantity)
        {
            if (Status != ProductionExecutionStatus.InProgress)
                throw new InvalidOperationException("只有进行中的执行记录才能更新数量");

            if (qualifiedQuantity < 0 || defectQuantity < 0)
                throw new ArgumentException("数量不能为负数");

            QualifiedQuantity = qualifiedQuantity;
            DefectQuantity = defectQuantity;
            Quantity = QualifiedQuantity + DefectQuantity;
        }

        /// <summary>
        /// 完成执行记录
        /// </summary>
        /// <param name="remarks">备注信息（可选）</param>
        /// <remarks>
        /// 将执行记录标记为已完成状态，同时：
        /// - 记录完成时间
        /// - 添加备注信息
        /// - 触发生产执行完成事件
        /// </remarks>
        /// <exception cref="InvalidOperationException">当执行记录不处于进行中状态或生产数量为0时抛出</exception>
        public void Complete(string remarks = null)
        {
            ValidateExecutionStatus();
            ValidateQuantity();
            Status = ProductionExecutionStatus.Completed;
            EndTime = DateTime.Now;
            Remarks = remarks;

            AddDistributedEvent(new ProductionExecutionCompletedEto
            {
                Id = Id,
                WorkOrderId = WorkOrderId,
                ExecutionNumber = ExecutionNumber,
                Quantity = Quantity,
                QualifiedQuantity = QualifiedQuantity,
                DefectQuantity = DefectQuantity,
                CompletedTime = EndTime
            });
        }

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <summary>
        /// 验证执行状态
        /// </summary>
        /// <remarks>
        /// 检查执行记录是否处于可更新的状态（进行中）
        /// </remarks>
        /// <exception cref="InvalidOperationException">当执行记录不处于进行中状态时抛出</exception>
        private void ValidateExecutionStatus()
        {
            if (Status != ProductionExecutionStatus.InProgress)
                throw new InvalidOperationException("只有进行中的执行记录才能更新");
        }

        private void ValidateQuantity()
        {
            if (Quantity == 0)
                throw new InvalidOperationException("生产数量为0，无法完成执行");
        }

        public void Cancel(string remarks)
        {
            if (Status != ProductionExecutionStatus.InProgress)
                throw new InvalidOperationException("只有进行中的执行记录才能取消");

            Status = ProductionExecutionStatus.Cancelled;
            EndTime = DateTime.Now;
            Remarks = remarks;
        }
    }
}