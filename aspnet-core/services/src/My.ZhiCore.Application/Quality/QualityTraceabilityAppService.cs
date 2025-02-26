using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量追溯应用服务
    /// </summary>
    public class QualityTraceabilityAppService : ApplicationService
    {
        private readonly IQualityTraceabilityManager _traceabilityManager;

        public QualityTraceabilityAppService(IQualityTraceabilityManager traceabilityManager)
        {            
            _traceabilityManager = traceabilityManager;
        }

        /// <summary>
        /// 创建质量追溯记录
        /// </summary>
        public async Task<Guid> CreateTraceabilityRecordAsync(
            string productCode,
            string processStep,
            string qualityData,
            DateTime recordTime)
        {
            return await _traceabilityManager.CreateTraceabilityRecordAsync(productCode, processStep, qualityData, recordTime);
        }

        /// <summary>
        /// 获取产品质量追溯链
        /// </summary>
        public async Task<List<QualityTraceabilityRecord>> GetProductTraceabilityChainAsync(
            string productCode,
            DateTime startTime,
            DateTime endTime)
        {
            return await _traceabilityManager.GetProductTraceabilityChainAsync(productCode, startTime, endTime);
        }

        /// <summary>
        /// 分析质量问题根因
        /// </summary>
        public async Task<RootCauseAnalysis> AnalyzeRootCauseAsync(
            Guid qualityIssueId,
            string analysisMethod)
        {
            return await _traceabilityManager.AnalyzeRootCauseAsync(qualityIssueId, analysisMethod);
        }

        /// <summary>
        /// 获取关联质量事件
        /// </summary>
        public async Task<List<RelatedQualityEvent>> GetRelatedQualityEventsAsync(
            string productCode,
            string processStep,
            DateTime startTime,
            DateTime endTime)
        {
            return await _traceabilityManager.GetRelatedQualityEventsAsync(productCode, processStep, startTime, endTime);
        }

        /// <summary>
        /// 生成质量追溯报告
        /// </summary>
        public async Task<QualityTraceabilityReport> GenerateTraceabilityReportAsync(
            string productCode,
            DateTime startTime,
            DateTime endTime,
            string reportType)
        {
            return await _traceabilityManager.GenerateTraceabilityReportAsync(productCode, startTime, endTime, reportType);
        }
    }
}