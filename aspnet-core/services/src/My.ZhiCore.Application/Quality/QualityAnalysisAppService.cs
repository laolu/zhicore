using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量分析应用服务
    /// </summary>
    public class QualityAnalysisAppService : ApplicationService
    {
        private readonly IQualityAnalysisManager _analysisManager;

        public QualityAnalysisAppService(IQualityAnalysisManager analysisManager)
        {
            _analysisManager = analysisManager;
        }

        /// <summary>
        /// 获取质量趋势分析
        /// </summary>
        public async Task<QualityTrendAnalysis> GetQualityTrendAsync(
            DateTime startTime,
            DateTime endTime,
            string analysisType)
        {
            return await _analysisManager.GetQualityTrendAsync(startTime, endTime, analysisType);
        }

        /// <summary>
        /// 获取质量问题分布统计
        /// </summary>
        public async Task<QualityIssueDistribution> GetIssueDistributionAsync(
            DateTime startTime,
            DateTime endTime,
            string groupBy)
        {
            return await _analysisManager.GetIssueDistributionAsync(startTime, endTime, groupBy);
        }

        /// <summary>
        /// 获取质量KPI指标
        /// </summary>
        public async Task<Dictionary<string, decimal>> GetQualityKPIAsync(
            DateTime startTime,
            DateTime endTime,
            List<string> kpiTypes)
        {
            return await _analysisManager.GetQualityKPIAsync(startTime, endTime, kpiTypes);
        }

        /// <summary>
        /// 生成质量分析报告
        /// </summary>
        public async Task<QualityAnalysisReport> GenerateAnalysisReportAsync(
            DateTime startTime,
            DateTime endTime,
            string reportType)
        {
            return await _analysisManager.GenerateAnalysisReportAsync(startTime, endTime, reportType);
        }
    }
}