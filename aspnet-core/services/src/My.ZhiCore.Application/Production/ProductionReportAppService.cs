using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产报表服务
    /// </summary>
    public class ProductionReportAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionReportAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 获取生产计划执行报表
        /// </summary>
        public async Task<ProductionExecutionReport> GetExecutionReportAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetProductionExecutionReportAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产质量报表
        /// </summary>
        public async Task<ProductionQualityReport> GetQualityReportAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetProductionQualityReportAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取设备利用率报表
        /// </summary>
        public async Task<EquipmentUtilizationReport> GetEquipmentUtilizationReportAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetEquipmentUtilizationReportAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产效率报表
        /// </summary>
        public async Task<ProductionEfficiencyReport> GetEfficiencyReportAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetProductionEfficiencyReportAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取生产成本报表
        /// </summary>
        public async Task<ProductionCostReport> GetCostReportAsync(DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetProductionCostReportAsync(startTime, endTime);
        }

        /// <summary>
        /// 导出生产报表
        /// </summary>
        public async Task<byte[]> ExportReportAsync(string reportType, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.ExportProductionReportAsync(reportType, startTime, endTime);
        }
    }
}