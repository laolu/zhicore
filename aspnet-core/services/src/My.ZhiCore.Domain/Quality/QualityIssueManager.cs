using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using My.ZhiCore.Quality.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量问题管理器
    /// </summary>
    public class QualityIssueManager : DomainService
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public QualityIssueManager(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 创建质量问题
        /// </summary>
        public async Task<Guid> CreateQualityIssueAsync(
            string issueType,
            string description,
            string severity,
            Guid discovererId,
            Guid? relatedItemId = null,
            Dictionary<string, string> extraProperties = null)
        {
            var id = GuidGenerator.Create();
            var eto = new QualityIssueCreatedEto
            {
                Id = id,
                IssueType = issueType,
                Description = description,
                Severity = severity,
                DiscoveryTime = Clock.Now,
                DiscovererId = discovererId,
                RelatedItemId = relatedItemId,
                ExtraProperties = extraProperties
            };

            await _distributedEventBus.PublishAsync(eto);
            return id;
        }

        /// <summary>
        /// 更新质量问题状态
        /// </summary>
        public async Task UpdateQualityIssueStatusAsync(
            Guid id,
            string newStatus,
            string oldStatus,
            string changeReason,
            Guid changerId)
        {
            var eto = new QualityIssueStatusChangedEto
            {
                Id = id,
                NewStatus = newStatus,
                OldStatus = oldStatus,
                ChangeReason = changeReason,
                ChangeTime = Clock.Now,
                ChangerId = changerId
            };

            await _distributedEventBus.PublishAsync(eto);
        }

        /// <summary>
        /// 处理质量问题
        /// </summary>
        public async Task ResolveQualityIssueAsync(
            Guid id,
            string solution,
            string result,
            Guid resolverId,
            string verificationResult,
            string preventiveMeasures)
        {
            var eto = new QualityIssueResolvedEto
            {
                Id = id,
                Solution = solution,
                Result = result,
                ResolveTime = Clock.Now,
                ResolverId = resolverId,
                VerificationResult = verificationResult,
                PreventiveMeasures = preventiveMeasures
            };

            await _distributedEventBus.PublishAsync(eto);
        }
    }
}