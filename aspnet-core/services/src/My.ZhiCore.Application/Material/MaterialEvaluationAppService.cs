using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料评估应用服务
    /// </summary>
    public class MaterialEvaluationAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialEvaluationAppService> _logger;

        public MaterialEvaluationAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialEvaluationAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料使用效率评估
        /// </summary>
        public async Task<MaterialEfficiencyEvaluationDto> CreateEfficiencyEvaluationAsync(CreateEfficiencyEvaluationDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料使用效率评估，物料ID：{MaterialId}", input.MaterialId);
                var evaluation = await _inventoryManager.CreateEfficiencyEvaluationAsync(
                    input.MaterialId,
                    input.StartDate,
                    input.EndDate,
                    input.UsageQuantity,
                    input.WasteQuantity);
                _logger.LogInformation("物料使用效率评估创建成功，评估ID：{Id}", evaluation.Id);
                return ObjectMapper.Map<MaterialEfficiencyEvaluation, MaterialEfficiencyEvaluationDto>(evaluation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料使用效率评估失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料使用效率评估失败", ex);
            }
        }

        /// <summary>
        /// 创建物料价值评估
        /// </summary>
        public async Task<MaterialValueEvaluationDto> CreateValueEvaluationAsync(CreateValueEvaluationDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料价值评估，物料ID：{MaterialId}", input.MaterialId);
                var evaluation = await _inventoryManager.CreateValueEvaluationAsync(
                    input.MaterialId,
                    input.EvaluationDate,
                    input.PurchasePrice,
                    input.MarketPrice,
                    input.StorageCost);
                _logger.LogInformation("物料价值评估创建成功，评估ID：{Id}", evaluation.Id);
                return ObjectMapper.Map<MaterialValueEvaluation, MaterialValueEvaluationDto>(evaluation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料价值评估失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料价值评估失败", ex);
            }
        }

        /// <summary>
        /// 获取物料评估报告
        /// </summary>
        public async Task<MaterialEvaluationReportDto> GetEvaluationReportAsync(Guid materialId, DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("开始获取物料评估报告，物料ID：{MaterialId}", materialId);
                var report = await _inventoryManager.GetEvaluationReportAsync(materialId, startDate, endDate);
                _logger.LogInformation("获取物料评估报告成功");
                return ObjectMapper.Map<MaterialEvaluationReport, MaterialEvaluationReportDto>(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料评估报告失败，物料ID：{MaterialId}", materialId);
                throw new UserFriendlyException("获取物料评估报告失败", ex);
            }
        }
    }
}