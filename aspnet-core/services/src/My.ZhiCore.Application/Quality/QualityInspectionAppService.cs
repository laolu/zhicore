using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using My.ZhiCore.Quality.Events;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验应用服务
    /// </summary>
    public class QualityInspectionAppService : ApplicationService
    {
        private readonly IQualityInspectionManager _inspectionManager;
        private readonly ILocalEventBus _localEventBus;

        public QualityInspectionAppService(
            IQualityInspectionManager inspectionManager,
            ILocalEventBus localEventBus)
        {
            _inspectionManager = inspectionManager;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建质量检验记录
        /// </summary>
        public async Task<QualityInspectionRecord> CreateInspectionRecordAsync(
            Guid inspectionItemId,
            string inspectionType,
            string result,
            bool isQualified,
            Dictionary<string, string> inspectionData,
            string remarks)
        {
            var record = await _inspectionManager.CreateInspectionRecordAsync(
                inspectionItemId,
                inspectionType,
                result,
                isQualified,
                inspectionData,
                remarks);

            await _localEventBus.PublishAsync(
                new QualityInspectionRecordCreatedEto
                {
                    Id = record.Id,
                    InspectionItemId = record.InspectionItemId,
                    InspectionType = record.InspectionType,
                    Result = record.Result,
                    IsQualified = record.IsQualified,
                    InspectionData = record.InspectionData,
                    InspectionTime = record.InspectionTime,
                    InspectorId = record.InspectorId,
                    Remarks = record.Remarks
                });

            return record;
        }

        /// <summary>
        /// 更新质量检验记录
        /// </summary>
        public async Task<QualityInspectionRecord> UpdateInspectionRecordAsync(
            Guid id,
            string result,
            bool isQualified,
            Dictionary<string, string> inspectionData,
            string remarks)
        {
            return await _inspectionManager.UpdateInspectionRecordAsync(
                id,
                result,
                isQualified,
                inspectionData,
                remarks);
        }

        /// <summary>
        /// 获取质量检验记录
        /// </summary>
        public async Task<QualityInspectionRecord> GetInspectionRecordAsync(Guid id)
        {
            return await _inspectionManager.GetInspectionRecordAsync(id);
        }

        /// <summary>
        /// 获取质量检验记录列表
        /// </summary>
        public async Task<List<QualityInspectionRecord>> GetInspectionRecordListAsync(Guid inspectionItemId)
        {
            return await _inspectionManager.GetInspectionRecordListAsync(inspectionItemId);
        }
    }
}