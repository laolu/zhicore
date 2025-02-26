using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产统计应用服务
    /// </summary>
    public class ProductionStatisticsAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionStatisticsAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 获取生产效率统计
        /// </summary>
        public async Task<List<EfficiencyIndicator>> GetEfficiencyStatisticsAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetEfficiencyStatisticsAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取设备利用率统计
        /// </summary>
        public async Task<Dictionary<Guid, double>> GetEquipmentUtilizationStatisticsAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetEquipmentUtilizationStatisticsAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产异常统计
        /// </summary>
        public async Task<List<ProductionAbnormalStatistics>> GetAbnormalStatisticsAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetAbnormalStatisticsAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产完成率统计
        /// </summary>
        public async Task<double> GetCompletionRateStatisticsAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetCompletionRateStatisticsAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产质量统计
        /// </summary>
        public async Task<Dictionary<string, int>> GetQualityStatisticsAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetQualityStatisticsAsync(startTime, endTime);
        }
    }
}