using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Events;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 工单实体 - 生产管理的核心聚合根
    /// </summary>
    /// <remarks>
    /// 该实体负责管理具体的生产任务，包含以下核心功能：
    /// - 工单基本信息管理（编号、产品、数量等）
    /// - 生产进度跟踪
    /// - 质量检验记录管理
    /// - 工单状态生命周期管理
    /// </remarks>
    public class WorkOrder : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        /// <remarks>用于唯一标识工单的业务编号，通常包含日期、产品等信息</remarks>
        public string OrderNumber { get; private set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        /// <remarks>关联的产品信息，用于标识该工单要生产的具体产品</remarks>
        public string ProductCode { get; private set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        /// <remarks>本次生产任务的计划生产数量，必须大于0</remarks>
        public int PlanQuantity { get; private set; }

        /// <summary>
        /// 实际生产数量
        /// </summary>
        /// <remarks>记录实际完成的生产数量，初始为0，随生产过程累计</remarks>
        public int ActualQuantity { get; private set; }

        /// <summary>
        /// 生产计划ID
        /// </summary>
        /// <remarks>关联的生产计划标识，指示该工单属于哪个生产计划</remarks>
        public Guid ProductionPlanId { get; private set; }

        /// <summary>
        /// 工单优先级
        /// </summary>
        /// <remarks>用于排序和调度的优先级值，数值越大优先级越高</remarks>
        public int Priority { get; private set; }

        /// <summary>
        /// 质量检验记录列表
        /// </summary>
        /// <remarks>该工单关联的所有质量检验记录，只读访问以保证数据一致性</remarks>
        private readonly List<QualityInspection> _qualityInspections;
        public IReadOnlyList<QualityInspection> QualityInspections => _qualityInspections.AsReadOnly();

        protected WorkOrder() 
        { 
            _qualityInspections = new List<QualityInspection>();
        }

        /// <summary>
        /// 创建新的工单
        /// </summary>
        /// <param name="id">工单唯一标识</param>
        /// <param name="orderNumber">工单编号</param>
        /// <param name="productCode">产品编码</param>
        /// <param name="planQuantity">计划数量</param>
        /// <param name="startTime">计划开始时间</param>
        /// <param name="endTime">计划结束时间</param>
        /// <param name="productionPlanId">生产计划ID</param>
        /// <param name="priority">优先级（默认为0）</param>
        /// <remarks>
        /// 创建新的工单实例，并进行参数验证：
        /// - 工单编号和产品编码不能为空
        /// - 计划数量必须大于0
        /// - 开始时间必须早于结束时间
        /// - 生产计划ID必须有效
        /// 新创建的工单默认为Created状态
        /// </remarks>
        public WorkOrder(
            Guid id,
            string orderNumber,
            string productCode,
            int planQuantity,
            DateTime startTime,
            DateTime endTime,
            Guid productionPlanId,
            int priority = 0)
        {
            ValidateConstructorParameters(orderNumber, productCode, planQuantity, startTime, endTime, productionPlanId);

            Id = id;
            OrderNumber = orderNumber;
            ProductCode = productCode;
            PlanQuantity = planQuantity;
            StartTime = startTime;
            EndTime = endTime;
            ProductionPlanId = productionPlanId;
            Priority = priority;
            Status = WorkOrderStatus.Created;
            ActualQuantity = 0;
            _qualityInspections = new List<QualityInspection>();

            AddLocalEvent(new WorkOrderCreatedEvent(Id));
        }

        /// <summary>
        /// 验证构造函数参数
        /// </summary>
        /// <remarks>
        /// 对工单的关键参数进行验证，确保数据的完整性和有效性
        /// </remarks>
        /// <exception cref="ArgumentException">当参数验证失败时抛出，并附带具体的错误信息</exception>
        private void ValidateConstructorParameters(
            string orderNumber,
            string productCode,
            int planQuantity,
            DateTime startTime,
            DateTime endTime,
            Guid productionPlanId)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new ArgumentException("工单编号不能为空", nameof(orderNumber));

            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("产品编码不能为空", nameof(productCode));

            if (planQuantity <= 0)
                throw new ArgumentException("计划数量必须大于0", nameof(planQuantity));

            if (startTime >= endTime)
                throw new ArgumentException("开始时间必须早于结束时间");

            if (productionPlanId == Guid.Empty)
                throw new ArgumentException("生产计划ID不能为空", nameof(productionPlanId));
        }
    }
}