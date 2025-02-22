using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量计划应用服务
    /// </summary>
    public class QualityPlanAppService : ApplicationService
    {
        private readonly IQualityPlanManager _planManager;

        public QualityPlanAppService(IQualityPlanManager planManager)
        {
            _planManager = planManager;
        }

        /// <summary>
        /// 创建质量计划
        /// </summary>
        public async Task<Guid> CreatePlanAsync(
            string planName,
            string planType,
            DateTime startTime,
            DateTime endTime,
            List<string> objectives)
        {
            return await _planManager.CreatePlanAsync(planName, planType, startTime, endTime, objectives);
        }

        /// <summary>
        /// 更新计划状态
        /// </summary>
        public async Task UpdatePlanStatusAsync(
            Guid planId,
            string status,
            string remarks)
        {
            await _planManager.UpdatePlanStatusAsync(planId, status, remarks);
        }

        /// <summary>
        /// 获取计划执行进度
        /// </summary>
        public async Task<Dictionary<string, decimal>> GetPlanProgressAsync(
            Guid planId)
        {
            return await _planManager.GetPlanProgressAsync(planId);
        }

        /// <summary>
        /// 获取计划列表
        /// </summary>
        public async Task<List<QualityPlan>> GetPlansAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            string planType = null)
        {
            return await _planManager.GetPlansAsync(startTime, endTime, planType);
        }

        /// <summary>
        /// 评估计划执行效果
        /// </summary>
        public async Task<PlanEvaluationReport> EvaluatePlanAsync(
            Guid planId)
        {
            return await _planManager.EvaluatePlanAsync(planId);
        }
    }
}