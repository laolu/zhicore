using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量预警应用服务
    /// </summary>
    public class QualityAlertAppService : ApplicationService
    {
        private readonly IQualityAlertManager _alertManager;

        public QualityAlertAppService(IQualityAlertManager alertManager)
        {            
            _alertManager = alertManager;
        }

        /// <summary>
        /// 创建预警规则
        /// </summary>
        public async Task<Guid> CreateAlertRuleAsync(
            string ruleName,
            string ruleType,
            string condition,
            string alertLevel,
            string notificationMethod)
        {
            return await _alertManager.CreateAlertRuleAsync(ruleName, ruleType, condition, alertLevel, notificationMethod);
        }

        /// <summary>
        /// 更新预警规则状态
        /// </summary>
        public async Task UpdateAlertRuleStatusAsync(
            Guid ruleId,
            bool isActive,
            string remarks)
        {
            await _alertManager.UpdateAlertRuleStatusAsync(ruleId, isActive, remarks);
        }

        /// <summary>
        /// 获取预警信息列表
        /// </summary>
        public async Task<List<QualityAlert>> GetAlertsAsync(
            DateTime startTime,
            DateTime endTime,
            string alertLevel)
        {
            return await _alertManager.GetAlertsAsync(startTime, endTime, alertLevel);
        }

        /// <summary>
        /// 处理预警信息
        /// </summary>
        public async Task HandleAlertAsync(
            Guid alertId,
            string handlingMethod,
            string handlingResult)
        {
            await _alertManager.HandleAlertAsync(alertId, handlingMethod, handlingResult);
        }

        /// <summary>
        /// 获取预警统计信息
        /// </summary>
        public async Task<Dictionary<string, int>> GetAlertStatisticsAsync(
            DateTime startTime,
            DateTime endTime,
            string groupBy)
        {
            return await _alertManager.GetAlertStatisticsAsync(startTime, endTime, groupBy);
        }
    }
}