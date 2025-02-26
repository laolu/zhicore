using System;
using System.Threading.Tasks;
using My.ZhiCore.Production.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 工单管理器 - 负责处理工单相关的领域逻辑和事件
    /// </summary>
    public class WorkOrderManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public WorkOrderManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 更新工单状态
        /// </summary>
        /// <param name="workOrder">工单实体</param>
        /// <param name="newStatus">新状态</param>
        /// <returns></returns>
        public async Task UpdateStatusAsync(WorkOrder workOrder, WorkOrderStatus newStatus)
        {
            var oldStatus = workOrder.Status;
            workOrder.Status = newStatus;

            await _localEventBus.PublishAsync(new WorkOrderStatusChangedEto
            {
                WorkOrderId = workOrder.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedTime = DateTime.Now
            });
        }

        /// <summary>
        /// 更新工单进度
        /// </summary>
        /// <param name="workOrder">工单实体</param>
        /// <param name="completedQuantity">已完成数量</param>
        /// <returns></returns>
        public async Task UpdateProgressAsync(WorkOrder workOrder, int completedQuantity)
        {
            if (completedQuantity > workOrder.PlannedQuantity)
            {
                throw new InvalidOperationException("完成数量不能超过计划数量");
            }

            workOrder.CompletedQuantity = completedQuantity;

            if (completedQuantity == workOrder.PlannedQuantity)
            {
                await UpdateStatusAsync(workOrder, WorkOrderStatus.Completed);
            }

            await _localEventBus.PublishAsync(new WorkOrderProgressUpdatedEto
            {
                WorkOrderId = workOrder.Id,
                CompletedQuantity = completedQuantity,
                UpdateTime = DateTime.Now
            });
        }
    }


}