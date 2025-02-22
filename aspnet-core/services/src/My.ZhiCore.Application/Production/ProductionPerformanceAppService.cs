using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产绩效服务
    /// </summary>
    public class ProductionPerformanceAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionPerformanceAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 获取生产线绩效
        /// </summary>
        public async Task<ProductionLinePerformance> GetLinePerformanceAsync(Guid lineId, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetProductionLinePerformanceAsync(lineId, startTime, endTime);
        }

        /// <summary>
        /// 获取员工生产绩效
        /// </summary>
        public async Task<EmployeePerformance> GetEmployeePerformanceAsync(Guid employeeId, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetEmployeePerformanceAsync(employeeId, startTime, endTime);
        }

        /// <summary>
        /// 获取设备绩效
        /// </summary>
        public async Task<EquipmentPerformance> GetEquipmentPerformanceAsync(Guid equipmentId, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetEquipmentPerformanceAsync(equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取生产计划完成率
        /// </summary>
        public async Task<double> GetPlanCompletionRateAsync(Guid planId)
        {
            return await _productionManager.GetPlanCompletionRateAsync(planId);
        }

        /// <summary>
        /// 获取生产质量达标率
        /// </summary>
        public async Task<double> GetQualityPassRateAsync(Guid productionId)
        {
            return await _productionManager.GetQualityPassRateAsync(productionId);
        }

        /// <summary>
        /// 获取生产效率指标
        /// </summary>
        public async Task<ProductionEfficiencyMetrics> GetEfficiencyMetricsAsync(Guid productionId)
        {
            return await _productionManager.GetProductionEfficiencyMetricsAsync(productionId);
        }

        /// <summary>
        /// 获取生产成本绩效
        /// </summary>
        public async Task<ProductionCostPerformance> GetCostPerformanceAsync(Guid productionId)
        {
            return await _productionManager.GetProductionCostPerformanceAsync(productionId);
        }
    }
}