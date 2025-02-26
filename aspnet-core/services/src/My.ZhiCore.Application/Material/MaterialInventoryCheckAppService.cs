using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料盘点应用服务
    /// </summary>
    public class MaterialInventoryCheckAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialInventoryCheckAppService> _logger;

        public MaterialInventoryCheckAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialInventoryCheckAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建盘点计划
        /// </summary>
        public async Task<MaterialInventoryCheckPlanDto> CreatePlanAsync(CreateInventoryCheckPlanDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料盘点计划，仓库ID：{WarehouseId}", input.WarehouseId);
                var plan = await _inventoryManager.CreateInventoryCheckPlanAsync(
                    input.WarehouseId,
                    input.LocationId,
                    input.PlanDate,
                    input.Description);
                _logger.LogInformation("物料盘点计划创建成功，ID：{Id}", plan.Id);
                return ObjectMapper.Map<MaterialInventoryCheckPlan, MaterialInventoryCheckPlanDto>(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料盘点计划失败，仓库ID：{WarehouseId}", input.WarehouseId);
                throw new UserFriendlyException("创建物料盘点计划失败", ex);
            }
        }

        /// <summary>
        /// 提交盘点结果
        /// </summary>
        public async Task<MaterialInventoryCheckResultDto> SubmitResultAsync(SubmitInventoryCheckResultDto input)
        {
            try
            {
                _logger.LogInformation("开始提交物料盘点结果，计划ID：{PlanId}", input.PlanId);
                var result = await _inventoryManager.SubmitInventoryCheckResultAsync(
                    input.PlanId,
                    input.MaterialId,
                    input.ActualQuantity,
                    input.Remark);
                _logger.LogInformation("物料盘点结果提交成功，ID：{Id}", result.Id);
                return ObjectMapper.Map<MaterialInventoryCheckResult, MaterialInventoryCheckResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "提交物料盘点结果失败，计划ID：{PlanId}", input.PlanId);
                throw new UserFriendlyException("提交物料盘点结果失败", ex);
            }
        }

        /// <summary>
        /// 获取盘点计划列表
        /// </summary>
        public async Task<List<MaterialInventoryCheckPlanDto>> GetPlanListAsync(Guid warehouseId)
        {
            var plans = await _inventoryManager.GetInventoryCheckPlanListAsync(warehouseId);
            return ObjectMapper.Map<List<MaterialInventoryCheckPlan>, List<MaterialInventoryCheckPlanDto>>(plans);
        }

        /// <summary>
        /// 获取盘点结果列表
        /// </summary>
        public async Task<List<MaterialInventoryCheckResultDto>> GetResultListAsync(Guid planId)
        {
            var results = await _inventoryManager.GetInventoryCheckResultListAsync(planId);
            return ObjectMapper.Map<List<MaterialInventoryCheckResult>, List<MaterialInventoryCheckResultDto>>(results);
        }
    }
}