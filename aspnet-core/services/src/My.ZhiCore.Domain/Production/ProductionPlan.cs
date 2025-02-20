using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MesNet.Production
{
    /// <summary>
    /// 生产计划实体 - 用于管理生产计划和排程
    /// </summary>
    /// <remarks>
    /// 该实体负责管理生产计划的创建、修改和执行，包含以下核心功能：
    /// - 生产计划的基本信息管理（编号、名称、时间等）
    /// - 生产计划与生产线的关联
    /// - 工单的创建和管理
    /// - 计划状态的生命周期管理
    /// </remarks>
    public class ProductionPlan : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        /// <remarks>用于唯一标识生产计划的业务编号，通常按照工厂编码规则生成</remarks>
        public string PlanNumber { get; private set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        /// <remarks>生产计划的显示名称，用于在系统中展示和识别</remarks>
        public string Name { get; private set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        /// <remarks>生产计划的预计开始执行时间</remarks>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        /// <remarks>生产计划的预计完成时间</remarks>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        /// <remarks>
        /// 表示生产计划当前的执行状态：
        /// - Draft：草稿状态，可以修改计划内容
        /// - Confirmed：已确认，不可修改
        /// - InProgress：执行中
        /// - Completed：已完成
        /// - Cancelled：已取消
        /// </remarks>
        public PlanStatus Status { get; private set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        /// <remarks>关联的生产线标识，指定该计划在哪条生产线上执行</remarks>
        public Guid ProductionLineId { get; private set; }

        /// <summary>
        /// 工单列表
        /// </summary>
        /// <remarks>该生产计划下的所有工单集合，只读访问以保证数据一致性</remarks>
        private readonly List<WorkOrder> _workOrders;
        public IReadOnlyList<WorkOrder> WorkOrders => _workOrders.AsReadOnly();

        protected ProductionPlan()
        {
            _workOrders = new List<WorkOrder>();
        }

        /// <summary>
        /// 创建新的生产计划
        /// </summary>
        /// <param name="id">生产计划唯一标识</param>
        /// <param name="planNumber">计划编号</param>
        /// <param name="name">计划名称</param>
        /// <param name="startTime">计划开始时间</param>
        /// <param name="endTime">计划结束时间</param>
        /// <param name="productionLineId">生产线ID</param>
        /// <remarks>
        /// 创建新的生产计划实例，并进行参数验证：
        /// - 计划编号和名称不能为空
        /// - 开始时间必须早于结束时间
        /// - 生产线ID必须有效
        /// 新创建的计划默认为草稿状态
        /// </remarks>
        public ProductionPlan(
            Guid id,
            string planNumber,
            string name,
            DateTime startTime,
            DateTime endTime,
            Guid productionLineId)
        {
            ValidateConstructorParameters(planNumber, name, startTime, endTime, productionLineId);

            Id = id;
            PlanNumber = planNumber;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            ProductionLineId = productionLineId;
            Status = PlanStatus.Draft;
            _workOrders = new List<WorkOrder>();

            AddLocalEvent(new ProductionPlanCreatedEvent(Id));
        }

        /// <summary>
        /// 验证构造函数参数
        /// </summary>
        /// <remarks>
        /// 对生产计划的关键参数进行验证，确保数据的完整性和有效性
        /// </remarks>
        /// <exception cref="ArgumentException">当参数验证失败时抛出，并附带具体的错误信息</exception>
        private void ValidateConstructorParameters(
            string planNumber,
            string name,
            DateTime startTime,
            DateTime endTime,
            Guid productionLineId)
        {
            if (string.IsNullOrWhiteSpace(planNumber))
                throw new ArgumentException("计划编号不能为空", nameof(planNumber));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("计划名称不能为空", nameof(name));

            if (startTime >= endTime)
                throw new ArgumentException("开始时间必须早于结束时间");

            if (productionLineId == Guid.Empty)
                throw new ArgumentException("生产线ID不能为空", nameof(productionLineId));
        }

        /// <summary>
        /// 添加工单
        /// </summary>
        /// <summary>
        /// 添加工单
        /// </summary>
        /// <remarks>
        /// 向生产计划中添加新的工单，仅在草稿状态下允许添加
        /// </remarks>
        /// <exception cref="InvalidOperationException">当计划不处于草稿状态时抛出</exception>
        public void AddWorkOrder(WorkOrder workOrder)
        {
            if (Status != PlanStatus.Draft)
                throw new InvalidOperationException("只有草稿状态的计划才能添加工单");

            _workOrders.Add(workOrder);
        }

        /// <summary>
        /// 移除工单
        /// </summary>
        public void RemoveWorkOrder(WorkOrder workOrder)
        {
            if (Status != PlanStatus.Draft)
                throw new InvalidOperationException("只有草稿状态的计划才能移除工单");

            _workOrders.Remove(workOrder);
        }

        /// <summary>
        /// 确认计划
        /// </summary>
        public void Confirm()
        {
            if (Status != PlanStatus.Draft)
                throw new InvalidOperationException("只有草稿状态的计划才能确认");

            if (_workOrders.Count == 0)
                throw new InvalidOperationException("计划中必须包含至少一个工单");

            Status = PlanStatus.Confirmed;
        }

        /// <summary>
        /// 开始执行计划
        /// </summary>
        public void Start()
        {
            if (Status != PlanStatus.Confirmed)
                throw new InvalidOperationException("只有已确认的计划才能开始执行");

            Status = PlanStatus.InProgress;
        }

        /// <summary>
        /// 完成计划
        /// </summary>
        public void Complete()
        {
            if (Status != PlanStatus.InProgress)
                throw new InvalidOperationException("只有进行中的计划才能完成");

            Status = PlanStatus.Completed;
        }

        /// <summary>
        /// 取消计划
        /// </summary>
        public void Cancel()
        {
            if (Status == PlanStatus.Completed)
                throw new InvalidOperationException("已完成的计划不能取消");

            Status = PlanStatus.Cancelled;
        }
    }

    public class ProductionPlanCreatedEvent : IEventData
    {
        public Guid PlanId { get; }

        public ProductionPlanCreatedEvent(Guid planId)
        {
            PlanId = planId;
        }
    }

    public class WorkOrderAddedToPlanEvent : IEventData
    {
        public Guid PlanId { get; }
        public Guid WorkOrderId { get; }

        public WorkOrderAddedToPlanEvent(Guid planId, Guid workOrderId)
        {
            PlanId = planId;
            WorkOrderId = workOrderId;
        }
    }
}