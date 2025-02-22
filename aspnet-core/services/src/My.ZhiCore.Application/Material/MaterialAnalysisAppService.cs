using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料分析服务，用于分析物料的使用趋势和消耗情况
    /// </summary>
    public class MaterialAnalysisAppService : ApplicationService
    {
        private readonly IRepository<MaterialInventory, Guid> _materialInventoryRepository;
        private readonly MaterialInventoryManager _materialInventoryManager;
        private readonly ILogger<MaterialAnalysisAppService> _logger;

        public MaterialAnalysisAppService(
            IRepository<MaterialInventory, Guid> materialInventoryRepository,
            MaterialInventoryManager materialInventoryManager,
            ILogger<MaterialAnalysisAppService> logger)
        {
            _materialInventoryRepository = materialInventoryRepository;
            _materialInventoryManager = materialInventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取物料消耗趋势分析
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>物料消耗趋势数据</returns>
        public async Task<List<MaterialConsumptionTrend>> GetConsumptionTrendAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始分析物料 {materialId} 的消耗趋势");
                var trends = await _materialInventoryManager.AnalyzeConsumptionTrendAsync(
                    materialId,
                    startTime,
                    endTime);
                _logger.LogInformation($"物料消耗趋势分析完成，共 {trends.Count} 条数据");
                return trends;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分析物料 {materialId} 消耗趋势失败");
                throw;
            }
        }

        /// <summary>
        /// 获取物料使用效率分析
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>物料使用效率数据</returns>
        public async Task<MaterialUsageEfficiency> GetUsageEfficiencyAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始分析物料 {materialId} 的使用效率");
                var efficiency = await _materialInventoryManager.AnalyzeUsageEfficiencyAsync(
                    materialId,
                    startTime,
                    endTime);
                _logger.LogInformation($"物料使用效率分析完成");
                return efficiency;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分析物料 {materialId} 使用效率失败");
                throw;
            }
        }

        /// <summary>
        /// 获取物料周转率分析
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>物料周转率数据</returns>
        public async Task<MaterialTurnoverRate> GetTurnoverRateAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始分析物料 {materialId} 的周转率");
                var turnoverRate = await _materialInventoryManager.AnalyzeTurnoverRateAsync(
                    materialId,
                    startTime,
                    endTime);
                _logger.LogInformation($"物料周转率分析完成");
                return turnoverRate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分析物料 {materialId} 周转率失败");
                throw;
            }
        }
    }
}