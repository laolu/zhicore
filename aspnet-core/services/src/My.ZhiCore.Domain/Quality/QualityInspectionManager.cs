using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using My.ZhiCore.Quality.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验管理器
    /// </summary>
    public class QualityInspectionManager : DomainService
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public QualityInspectionManager(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 创建质量检验记录
        /// </summary>
        public async Task<Guid> CreateQualityInspectionRecordAsync(
            Guid inspectionItemId,
            string inspectionType,
            string result,
            bool isQualified,
            Dictionary<string, string> inspectionData,
            Guid inspectorId,
            string remarks = null)
        {
            var id = GuidGenerator.Create();
            var eto = new QualityInspectionRecordCreatedEto
            {
                Id = id,
                InspectionItemId = inspectionItemId,
                InspectionType = inspectionType,
                Result = result,
                IsQualified = isQualified,
                InspectionData = inspectionData,
                InspectionTime = Clock.Now,
                InspectorId = inspectorId,
                Remarks = remarks
            };

            await _distributedEventBus.PublishAsync(eto);
            return id;
        }

        /// <summary>
        /// 更新质量检验结果
        /// </summary>
        public async Task UpdateQualityInspectionResultAsync(
            Guid id,
            string updatedResult,
            bool updatedIsQualified,
            string updateReason,
            Guid updaterId)
        {
            var eto = new QualityInspectionResultUpdatedEto
            {
                Id = id,
                UpdatedResult = updatedResult,
                UpdatedIsQualified = updatedIsQualified,
                UpdateReason = updateReason,
                UpdateTime = Clock.Now,
                UpdaterId = updaterId
            };

            await _distributedEventBus.PublishAsync(eto);
        }
    }
}