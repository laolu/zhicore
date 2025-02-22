using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using My.ZhiCore.Quality.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量成本管理器
    /// </summary>
    public class QualityCostManager : DomainService
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public QualityCostManager(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 记录质量成本
        /// </summary>
        public async Task<Guid> RecordQualityCostAsync(
            string costType,
            decimal amount,
            string description,
            Guid recorderId,
            Guid? relatedQualityIssueId = null,
            Dictionary<string, string> extraProperties = null)
        {
            var id = GuidGenerator.Create();
            var eto = new QualityCostRecordedEto
            {
                Id = id,
                CostType = costType,
                Amount = amount,
                Description = description,
                OccurredTime = Clock.Now,
                RelatedQualityIssueId = relatedQualityIssueId,
                RecorderId = recorderId,
                ExtraProperties = extraProperties
            };

            await _distributedEventBus.PublishAsync(eto);
            return id;
        }

        /// <summary>
        /// 计算质量成本统计
        /// </summary>
        public async Task<Guid> CalculateQualityCostStatisticsAsync(
            DateTime startTime,
            DateTime endTime,
            decimal preventionCostTotal,
            decimal appraisalCostTotal,
            decimal internalFailureCostTotal,
            decimal externalFailureCostTotal,
            Guid calculatorId)
        {
            var id = GuidGenerator.Create();
            var eto = new QualityCostStatisticsCalculatedEto
            {
                Id = id,
                StartTime = startTime,
                EndTime = endTime,
                PreventionCostTotal = preventionCostTotal,
                AppraisalCostTotal = appraisalCostTotal,
                InternalFailureCostTotal = internalFailureCostTotal,
                ExternalFailureCostTotal = externalFailureCostTotal,
                CalculationTime = Clock.Now,
                CalculatorId = calculatorId
            };

            await _distributedEventBus.PublishAsync(eto);
            return id;
        }
    }
}