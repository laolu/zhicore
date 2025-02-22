using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产监控服务
    /// </summary>
    public class ProductionMonitoringAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionMonitoringAppService(ProductionManager productionManager)
        {   
            _productionManager = productionManager;
        }

        /// <summary>
        /// 获取生产状态
        /// </summary>
        public async Task<ProductionStatus> GetStatusAsync(Guid id)
        {
            return await _productionManager.GetProductionStatusAsync(id);
        }

        /// <summary>
        /// 获取生产性能指标
        /// </summary>
        public async Task<ProductionMetrics> GetMetricsAsync(Guid id)
        {
            return await _productionManager.GetProductionMetricsAsync(id);
        }

        /// <summary>
        /// 获取生产警报列表
        /// </summary>
        public async Task<List<ProductionAlert>> GetAlertsAsync(Guid id)
        {
            return await _productionManager.GetProductionAlertsAsync(id);
        }

        /// <summary>
        /// 获取生产设备状态
        /// </summary>
        public async Task<List<EquipmentStatus>> GetEquipmentStatusAsync(Guid id)
        {
            return await _productionManager.GetEquipmentStatusAsync(id);
        }

        /// <summary>
        /// 获取实时生产数据
        /// </summary>
        public async Task<ProductionRealTimeData> GetRealTimeDataAsync(Guid id)
        {
            return await _productionManager.GetProductionRealTimeDataAsync(id);
        }
    }
}