using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量改进应用服务
    /// </summary>
    public class QualityImprovementAppService : ApplicationService
    {
        private readonly IQualityImprovementManager _improvementManager;

        public QualityImprovementAppService(IQualityImprovementManager improvementManager)
        {            
            _improvementManager = improvementManager;
        }

        /// <summary>
        /// 创建改进建议
        /// </summary>
        public async Task<Guid> CreateImprovementSuggestionAsync(
            string title,
            string description,
            string category,
            string priority,
            string expectedBenefit)
        {
            return await _improvementManager.CreateImprovementSuggestionAsync(
                title, description, category, priority, expectedBenefit);
        }

        /// <summary>
        /// 创建实施计划
        /// </summary>
        public async Task<Guid> CreateImplementationPlanAsync(
            Guid suggestionId,
            string planName,
            DateTime startTime,
            DateTime endTime,
            List<string> actions)
        {
            return await _improvementManager.CreateImplementationPlanAsync(
                suggestionId, planName, startTime, endTime, actions);
        }

        /// <summary>
        /// 更新实施进度
        /// </summary>
        public async Task UpdateImplementationProgressAsync(
            Guid planId,
            decimal progress,
            string status,
            string remarks)
        {
            await _improvementManager.UpdateImplementationProgressAsync(planId, progress, status, remarks);
        }

        /// <summary>
        /// 获取改进效果评估
        /// </summary>
        public async Task<ImprovementEffectiveness> EvaluateImprovementEffectAsync(
            Guid planId,
            DateTime evaluationTime)
        {
            return await _improvementManager.EvaluateImprovementEffectAsync(planId, evaluationTime);
        }

        /// <summary>
        /// 获取改进统计信息
        /// </summary>
        public async Task<Dictionary<string, int>> GetImprovementStatisticsAsync(
            DateTime startTime,
            DateTime endTime,
            string groupBy)
        {
            return await _improvementManager.GetImprovementStatisticsAsync(startTime, endTime, groupBy);
        }
    }
}