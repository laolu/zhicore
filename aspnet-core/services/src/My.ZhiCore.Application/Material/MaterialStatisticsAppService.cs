using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料统计应用服务
    /// </summary>
    public class MaterialStatisticsAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialStatisticsAppService> _logger;

        public MaterialStatisticsAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialStatisticsAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取物料库存统计
        /// </summary>
        public async Task<MaterialInventoryStatisticsDto> GetInventoryStatisticsAsync(Guid? categoryId = null)
        {
            try
            {
                _logger.LogInformation("开始获取物料库存统计数据，分类ID：{CategoryId}", categoryId);
                var statistics = await _inventoryManager.GetInventoryStatisticsAsync(categoryId);
                _logger.LogInformation("获取物料库存统计数据成功");
                return ObjectMapper.Map<MaterialInventoryStatistics, MaterialInventoryStatisticsDto>(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料库存统计数据失败");
                throw new UserFriendlyException("获取物料库存统计数据失败", ex);
            }
        }

        /// <summary>
        /// 获取物料周转率统计
        /// </summary>
        public async Task<MaterialTurnoverStatisticsDto> GetTurnoverStatisticsAsync(DateTime startDate, DateTime endDate, Guid? categoryId = null)
        {
            try
            {
                _logger.LogInformation("开始获取物料周转率统计数据，开始日期：{StartDate}，结束日期：{EndDate}", startDate, endDate);
                var statistics = await _inventoryManager.GetTurnoverStatisticsAsync(startDate, endDate, categoryId);
                _logger.LogInformation("获取物料周转率统计数据成功");
                return ObjectMapper.Map<MaterialTurnoverStatistics, MaterialTurnoverStatisticsDto>(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料周转率统计数据失败");
                throw new UserFriendlyException("获取物料周转率统计数据失败", ex);
            }
        }

        /// <summary>
        /// 获取物料库存预警统计
        /// </summary>
        public async Task<MaterialInventoryAlertStatisticsDto> GetInventoryAlertStatisticsAsync(Guid? categoryId = null)
        {
            try
            {
                _logger.LogInformation("开始获取物料库存预警统计数据，分类ID：{CategoryId}", categoryId);
                var statistics = await _inventoryManager.GetInventoryAlertStatisticsAsync(categoryId);
                _logger.LogInformation("获取物料库存预警统计数据成功");
                return ObjectMapper.Map<MaterialInventoryAlertStatistics, MaterialInventoryAlertStatisticsDto>(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料库存预警统计数据失败");
                throw new UserFriendlyException("获取物料库存预警统计数据失败", ex);
            }
        }
    }
}