using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产调度应用服务
    /// </summary>
    public class ProductionSchedulingAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionSchedulingAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 分配生产任务
        /// </summary>
        public async Task<Production> AssignProductionTaskAsync(Guid productionId, Guid productionLineId)
        {
            return await _productionManager.AssignProductionTaskAsync(productionId, productionLineId);
        }

        /// <summary>
        /// 设置任务优先级
        /// </summary>
        public async Task<Production> SetTaskPriorityAsync(Guid productionId, int priority)
        {
            return await _productionManager.SetTaskPriorityAsync(productionId, priority);
        }

        /// <summary>
        /// 调整生产计划
        /// </summary>
        public async Task<Production> AdjustProductionPlanAsync(Guid productionId, DateTime newStartTime, DateTime newEndTime)
        {
            return await _productionManager.AdjustProductionPlanAsync(productionId, newStartTime, newEndTime);
        }

        /// <summary>
        /// 获取生产线任务列表
        /// </summary>
        public async Task<List<Production>> GetProductionLineTasksAsync(Guid productionLineId)
        {
            return await _productionManager.GetProductionLineTasksAsync(productionLineId);
        }

        /// <summary>
        /// 获取待调度任务列表
        /// </summary>
        public async Task<List<Production>> GetPendingTasksAsync()
        {
            return await _productionManager.GetPendingTasksAsync();
        }

        /// <summary>
        /// 资源调度
        /// </summary>
        public async Task<bool> ScheduleResourcesAsync(Guid productionId, Dictionary<string, object> resources)
        {
            return await _productionManager.ScheduleResourcesAsync(productionId, resources);
        }
    }
}