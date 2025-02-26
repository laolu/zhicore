using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料调度应用服务
    /// </summary>
    public class MaterialSchedulingAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialSchedulingAppService> _logger;

        public MaterialSchedulingAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialSchedulingAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建调度计划
        /// </summary>
        public async Task<MaterialSchedulingPlanDto> CreateSchedulingPlanAsync(CreateSchedulingPlanDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料调度计划，物料ID：{MaterialId}", input.MaterialId);
                var plan = await _inventoryManager.CreateSchedulingPlanAsync(
                    input.MaterialId,
                    input.SourceWarehouseId,
                    input.TargetWarehouseId,
                    input.Quantity,
                    input.ScheduledDate,
                    input.Priority);
                _logger.LogInformation("物料调度计划创建成功，计划ID：{Id}", plan.Id);
                return ObjectMapper.Map<MaterialSchedulingPlan, MaterialSchedulingPlanDto>(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料调度计划失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料调度计划失败", ex);
            }
        }

        /// <summary>
        /// 执行调度计划
        /// </summary>
        public async Task<MaterialSchedulingPlanDto> ExecuteSchedulingPlanAsync(Guid planId)
        {
            try
            {
                _logger.LogInformation("开始执行物料调度计划，计划ID：{PlanId}", planId);
                var plan = await _inventoryManager.ExecuteSchedulingPlanAsync(planId);
                _logger.LogInformation("物料调度计划执行成功，计划ID：{Id}", plan.Id);
                return ObjectMapper.Map<MaterialSchedulingPlan, MaterialSchedulingPlanDto>(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行物料调度计划失败，计划ID：{PlanId}", planId);
                throw new UserFriendlyException("执行物料调度计划失败", ex);
            }
        }

        /// <summary>
        /// 取消调度计划
        /// </summary>
        public async Task CancelSchedulingPlanAsync(Guid planId)
        {
            try
            {
                _logger.LogInformation("开始取消物料调度计划，计划ID：{PlanId}", planId);
                await _inventoryManager.CancelSchedulingPlanAsync(planId);
                _logger.LogInformation("物料调度计划取消成功，计划ID：{PlanId}", planId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取消物料调度计划失败，计划ID：{PlanId}", planId);
                throw new UserFriendlyException("取消物料调度计划失败", ex);
            }
        }
    }
}