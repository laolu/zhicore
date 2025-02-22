using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量成本应用服务
    /// </summary>
    public class QualityCostAppService : ApplicationService
    {
        private readonly IQualityCostManager _costManager;

        public QualityCostAppService(IQualityCostManager costManager)
        {            
            _costManager = costManager;
        }

        /// <summary>
        /// 记录质量成本
        /// </summary>
        public async Task<Guid> RecordCostAsync(
            string costType,
            decimal amount,
            string description,
            DateTime occurredTime)
        {
            return await _costManager.RecordCostAsync(costType, amount, description, occurredTime);
        }

        /// <summary>
        /// 获取质量成本统计
        /// </summary>
        public async Task<Dictionary<string, decimal>> GetCostStatisticsAsync(
            DateTime startTime,
            DateTime endTime,
            string groupBy)
        {
            return await _costManager.GetCostStatisticsAsync(startTime, endTime, groupBy);
        }

        /// <summary>
        /// 分析成本趋势
        /// </summary>
        public async Task<Dictionary<string, List<decimal>>> AnalyzeCostTrendAsync(
            DateTime startTime,
            DateTime endTime,
            string costType)
        {
            return await _costManager.AnalyzeCostTrendAsync(startTime, endTime, costType);
        }

        /// <summary>
        /// 获取成本分析报告
        /// </summary>
        public async Task<QualityCostReport> GenerateCostReportAsync(
            DateTime startTime,
            DateTime endTime)
        {
            return await _costManager.GenerateCostReportAsync(startTime, endTime);
        }
    }
}