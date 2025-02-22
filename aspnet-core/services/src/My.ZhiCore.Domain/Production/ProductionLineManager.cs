using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产线管理器 - 负责处理生产线相关的领域逻辑和事件
    /// </summary>
    public class ProductionLineManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public ProductionLineManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 更新生产线状态
        /// </summary>
        /// <param name="productionLine">生产线实体</param>
        /// <param name="newStatus">新状态</param>
        /// <returns></returns>
        public async Task UpdateStatusAsync(ProductionLine productionLine, ProductionLineStatus newStatus)
        {
            var oldStatus = productionLine.Status;
            productionLine.Status = newStatus;

            await _localEventBus.PublishAsync(new ProductionLineStatusChangedEto
            {
                ProductionLineId = productionLine.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedTime = DateTime.Now
            });
        }

        /// <summary>
        /// 分配生产任务到生产线
        /// </summary>
        /// <param name="productionLine">生产线实体</param>
        /// <param name="workOrder">工单</param>
        /// <returns></returns>
        public async Task AssignWorkOrderAsync(ProductionLine productionLine, WorkOrder workOrder)
        {
            if (productionLine.Status != ProductionLineStatus.Idle)
            {
                throw new InvalidOperationException("只有空闲状态的生产线才能分配新的工单");
            }

            await UpdateStatusAsync(productionLine, ProductionLineStatus.Running);

            await _localEventBus.PublishAsync(new WorkOrderAssignedToProductionLineEto
            {
                ProductionLineId = productionLine.Id,
                WorkOrderId = workOrder.Id,
                AssignedTime = DateTime.Now
            });
        }
    }

    /// <summary>
    /// 生产线状态变更事件
    /// </summary>
    public class ProductionLineStatusChangedEto
    {
        public Guid ProductionLineId { get; set; }
        public ProductionLineStatus OldStatus { get; set; }
        public ProductionLineStatus NewStatus { get; set; }
        public DateTime ChangedTime { get; set; }
    }

    /// <summary>
    /// 工单分配到生产线事件
    /// </summary>
    public class WorkOrderAssignedToProductionLineEto
    {
        public Guid ProductionLineId { get; set; }
        public Guid WorkOrderId { get; set; }
        public DateTime AssignedTime { get; set; }
    }
}