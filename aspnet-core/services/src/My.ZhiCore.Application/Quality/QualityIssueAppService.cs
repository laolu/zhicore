using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using My.ZhiCore.Quality.Events;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量问题应用服务
    /// </summary>
    public class QualityIssueAppService : ApplicationService
    {
        private readonly IQualityIssueManager _issueManager;
        private readonly ILocalEventBus _localEventBus;

        public QualityIssueAppService(
            IQualityIssueManager issueManager,
            ILocalEventBus localEventBus)
        {
            _issueManager = issueManager;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建质量问题
        /// </summary>
        public async Task<QualityIssue> CreateIssueAsync(
            string issueType,
            string description,
            string severity,
            Guid? relatedItemId,
            Dictionary<string, string> extraProperties,
            string remarks)
        {
            var issue = await _issueManager.CreateIssueAsync(
                issueType,
                description,
                severity,
                relatedItemId,
                extraProperties,
                remarks);

            await _localEventBus.PublishAsync(
                new QualityIssueCreatedEto
                {
                    Id = issue.Id,
                    IssueType = issue.IssueType,
                    Description = issue.Description,
                    Severity = issue.Severity,
                    DiscoveryTime = issue.DiscoveryTime,
                    DiscovererId = issue.DiscovererId,
                    RelatedItemId = issue.RelatedItemId,
                    ExtraProperties = issue.ExtraProperties
                });

            return issue;
        }

        /// <summary>
        /// 更新质量问题
        /// </summary>
        public async Task<QualityIssue> UpdateIssueAsync(
            Guid id,
            string description,
            string severity,
            Dictionary<string, string> extraProperties,
            string remarks)
        {
            return await _issueManager.UpdateIssueAsync(
                id,
                description,
                severity,
                extraProperties,
                remarks);
        }

        /// <summary>
        /// 获取质量问题
        /// </summary>
        public async Task<QualityIssue> GetIssueAsync(Guid id)
        {
            return await _issueManager.GetIssueAsync(id);
        }

        /// <summary>
        /// 获取质量问题列表
        /// </summary>
        public async Task<List<QualityIssue>> GetIssueListAsync(string issueType = null)
        {
            return await _issueManager.GetIssueListAsync(issueType);
        }

        /// <summary>
        /// 关闭质量问题
        /// </summary>
        public async Task CloseIssueAsync(Guid id, string resolution)
        {
            await _issueManager.CloseIssueAsync(id, resolution);
        }
    }
}