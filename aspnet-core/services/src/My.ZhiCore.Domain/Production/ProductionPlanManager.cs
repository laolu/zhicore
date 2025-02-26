using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产计划管理器 - 负责处理生产计划相关的领域逻辑和事件
    /// </summary>
    public class ProductionPlanManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;
        private readonly WorkOrderManager _workOrderManager;

        public ProductionPlanManager(
            ILocalEventBus localEventBus,
            WorkOrderManager workOrderManager)
        {
            _localEventBus = localEventBus;
            _workOrderManager = workOrderManager;
        }

        /// <summary>
        /// 更新生产计划状态
        /// </summary>
        /// <param name="productionPlan">生产计划实体</param>
        /// <param name="newStatus">新状态</param>
        /// <returns></returns>
        public async Task UpdateStatusAsync(ProductionPlan productionPlan, ProductionPlanStatus newStatus)
        {
            var oldStatus = productionPlan.Status;
            productionPlan.Status = newStatus;

            await _localEventBus.PublishAsync(new ProductionPlanStatusChangedEto
            {
                ProductionPlanId = productionPlan.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedTime = DateTime.Now
            });
        }

        /// <summary>
        /// 创建工单
        /// </summary>
        /// <param name="productionPlan">生产计划</param>
        /// <param name="workOrder">工单</param>
        /// <returns></returns>
        public async Task CreateWorkOrderAsync(ProductionPlan productionPlan, WorkOrder workOrder)
        {
            if (productionPlan.Status != ProductionPlanStatus.InProgress)
            {
                throw new InvalidOperationException("只有进行中的生产计划才能创建工单");
            }

            await _workOrderManager.UpdateStatusAsync(workOrder, WorkOrderStatus.Created);

            await _localEventBus.PublishAsync(new WorkOrderCreatedFromPlanEto
            {
                ProductionPlanId = productionPlan.Id,
                WorkOrderId = workOrder.Id,
                CreatedTime = DateTime.Now
            });
        }
    }

    /// <summary>
    /// 生产计划状态
    /// </summary>
    public enum ProductionPlanStatus
    {
        Draft = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }

    /// <summary>
    /// 生产计划状态变更事件
    /// </summary>
    public class ProductionPlanStatusChangedEto
    {
        public Guid ProductionPlanId { get; set; }
        public ProductionPlanStatus OldStatus { get; set; }
        public ProductionPlanStatus NewStatus { get; set; }
        public DateTime ChangedTime { get; set; }
    }

    /// <summary>
    /// 从生产计划创建工单事件
    /// </summary>
    public class WorkOrderCreatedFromPlanEto
    {
        public Guid ProductionPlanId { get; set; }
        public Guid WorkOrderId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}