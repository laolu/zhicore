using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 看板显示应用服务
    /// </summary>
    public class KanbanDisplayAppService : ApplicationService
    {
        private readonly KanbanManager _kanbanManager;

        public KanbanDisplayAppService(KanbanManager kanbanManager)
        {
            _kanbanManager = kanbanManager;
        }

        /// <summary>
        /// 获取看板实时数据
        /// </summary>
        public async Task<KanbanData> GetKanbanDataAsync(Guid id)
        {
            return await _kanbanManager.GetKanbanDataAsync(id);
        }

        /// <summary>
        /// 获取看板历史数据
        /// </summary>
        public async Task<List<KanbanData>> GetKanbanHistoryDataAsync(Guid id, DateTime startTime, DateTime endTime)
        {
            return await _kanbanManager.GetKanbanHistoryDataAsync(id, startTime, endTime);
        }

        /// <summary>
        /// 获取看板统计数据
        /// </summary>
        public async Task<KanbanStatistics> GetKanbanStatisticsAsync(Guid id, DateTime startTime, DateTime endTime)
        {
            return await _kanbanManager.GetKanbanStatisticsAsync(id, startTime, endTime);
        }

        /// <summary>
        /// 订阅看板数据更新
        /// </summary>
        public async Task<bool> SubscribeKanbanDataAsync(Guid id)
        {
            return await _kanbanManager.SubscribeKanbanDataAsync(id);
        }

        /// <summary>
        /// 取消订阅看板数据更新
        /// </summary>
        public async Task<bool> UnsubscribeKanbanDataAsync(Guid id)
        {
            return await _kanbanManager.UnsubscribeKanbanDataAsync(id);
        }

        /// <summary>
        /// 设置看板数据刷新间隔
        /// </summary>
        public async Task<bool> SetKanbanRefreshIntervalAsync(Guid id, int intervalSeconds)
        {
            return await _kanbanManager.SetKanbanRefreshIntervalAsync(id, intervalSeconds);
        }

        /// <summary>
        /// 获取看板数据刷新间隔
        /// </summary>
        public async Task<int> GetKanbanRefreshIntervalAsync(Guid id)
        {
            return await _kanbanManager.GetKanbanRefreshIntervalAsync(id);
        }

        /// <summary>
        /// 设置看板数据告警规则
        /// </summary>
        public async Task<bool> SetKanbanAlertRulesAsync(Guid id, List<KanbanAlertRule> rules)
        {
            return await _kanbanManager.SetKanbanAlertRulesAsync(id, rules);
        }

        /// <summary>
        /// 获取看板数据告警规则
        /// </summary>
        public async Task<List<KanbanAlertRule>> GetKanbanAlertRulesAsync(Guid id)
        {
            return await _kanbanManager.GetKanbanAlertRulesAsync(id);
        }
    }
}