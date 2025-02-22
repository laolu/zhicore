using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料成本服务，用于管理物料的成本核算和分析
    /// </summary>
    public class MaterialCostAppService : ApplicationService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly MaterialInventoryManager _materialInventoryManager;
        private readonly ILogger<MaterialCostAppService> _logger;

        public MaterialCostAppService(
            IRepository<Material, Guid> materialRepository,
            MaterialInventoryManager materialInventoryManager,
            ILogger<MaterialCostAppService> logger)
        {
            _materialRepository = materialRepository;
            _materialInventoryManager = materialInventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 计算物料的平均采购成本
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>平均采购成本</returns>
        public async Task<decimal> CalculateAveragePurchaseCostAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始计算物料 {materialId} 的平均采购成本");
                var material = await _materialRepository.GetAsync(materialId);
                var purchaseRecords = await _materialInventoryManager.GetPurchaseRecordsAsync(
                    materialId,
                    startTime,
                    endTime);

                decimal totalCost = 0;
                int totalQuantity = 0;

                foreach (var record in purchaseRecords)
                {
                    totalCost += record.UnitPrice * record.Quantity;
                    totalQuantity += record.Quantity;
                }

                var averageCost = totalQuantity > 0 ? totalCost / totalQuantity : 0;
                _logger.LogInformation($"物料 {material.Name} 的平均采购成本计算完成");

                return averageCost;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"计算物料 {materialId} 平均采购成本失败");
                throw;
            }
        }

        /// <summary>
        /// 分析物料成本趋势
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>成本趋势数据</returns>
        public async Task<List<MaterialCostTrend>> AnalyzeCostTrendAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始分析物料 {materialId} 的成本趋势");
                var material = await _materialRepository.GetAsync(materialId);
                var costTrends = await _materialInventoryManager.AnalyzeCostTrendAsync(
                    materialId,
                    startTime,
                    endTime);

                _logger.LogInformation($"物料 {material.Name} 的成本趋势分析完成");
                return costTrends;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分析物料 {materialId} 成本趋势失败");
                throw;
            }
        }

        /// <summary>
        /// 计算物料库存持有成本
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>库存持有成本</returns>
        public async Task<decimal> CalculateInventoryHoldingCostAsync(
            Guid materialId,
            DateTime startTime,
            DateTime endTime)
        {
            try
            {
                _logger.LogInformation($"开始计算物料 {materialId} 的库存持有成本");
                var material = await _materialRepository.GetAsync(materialId);
                var holdingCost = await _materialInventoryManager.CalculateHoldingCostAsync(
                    materialId,
                    startTime,
                    endTime);

                _logger.LogInformation($"物料 {material.Name} 的库存持有成本计算完成");
                return holdingCost;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"计算物料 {materialId} 库存持有成本失败");
                throw;
            }
        }
    }
}