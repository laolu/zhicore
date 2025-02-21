using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量统计分析领域服务，负责管理质量统计数据的生命周期和业务规则
    /// </summary>
    public class QualityStatisticsManager : DomainService
    {
        private readonly IRepository<QualityStatistics, Guid> _qualityStatisticsRepository;
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILocalEventBus _localEventBus;

        public QualityStatisticsManager(
            IRepository<QualityStatistics, Guid> qualityStatisticsRepository,
            IRepository<Equipment, Guid> equipmentRepository,
            ILocalEventBus localEventBus)
        {
            _qualityStatisticsRepository = qualityStatisticsRepository;
            _equipmentRepository = equipmentRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建质量统计记录
        /// </summary>
        public async Task<QualityStatistics> CreateAsync(
            Guid equipmentId,
            string productCode,
            string processCode,
            string batchNumber,
            int totalCount,
            int defectCount,
            string mainDefectType,
            string improvementSuggestion)
        {
            // 验证设备是否存在
            var equipment = await _equipmentRepository.GetAsync(equipmentId);

            // 计算合格率
            var qualifiedRate = totalCount > 0 
                ? decimal.Round(((decimal)(totalCount - defectCount) / totalCount) * 100, 2)
                : 0;

            // 分析质量趋势
            var qualityTrend = await AnalyzeQualityTrendAsync(equipmentId, qualifiedRate);

            // 生成分析报告
            var analysisReport = GenerateAnalysisReport(
                qualifiedRate,
                qualityTrend,
                mainDefectType,
                improvementSuggestion);

            var statistics = new QualityStatistics(
                GuidGenerator.Create(),
                equipmentId,
                Clock.Now,
                productCode,
                processCode,
                batchNumber,
                totalCount,
                defectCount,
                qualifiedRate,
                mainDefectType,
                qualityTrend,
                improvementSuggestion,
                analysisReport);

            await _qualityStatisticsRepository.InsertAsync(statistics);

            // 发布质量趋势变化事件
            await PublishQualityTrendChangedEventAsync(statistics);

            // 检查是否需要发出质量预警
            await CheckAndPublishQualityAlertAsync(statistics);

            return statistics;
        }

        /// <summary>
        /// 分析质量趋势
        /// </summary>
        private async Task<QualityTrend> AnalyzeQualityTrendAsync(Guid equipmentId, decimal currentQualifiedRate)
        {
            var previousStatistics = await _qualityStatisticsRepository.FirstOrDefaultAsync(
                x => x.EquipmentId == equipmentId,
                orderBy: x => x.OrderByDescending(s => s.StatisticsTime));

            if (previousStatistics == null)
            {
                return QualityTrend.Stable;
            }

            var difference = currentQualifiedRate - previousStatistics.QualifiedRate;
            if (difference > 1)
            {
                return QualityTrend.Rising;
            }
            else if (difference < -1)
            {
                return QualityTrend.Declining;
            }

            return QualityTrend.Stable;
        }

        /// <summary>
        /// 生成分析报告
        /// </summary>
        private string GenerateAnalysisReport(
            decimal qualifiedRate,
            QualityTrend qualityTrend,
            string mainDefectType,
            string improvementSuggestion)
        {
            return $"质量分析报告:\n" +
                   $"合格率: {qualifiedRate}%\n" +
                   $"质量趋势: {qualityTrend}\n" +
                   $"主要缺陷类型: {mainDefectType}\n" +
                   $"改进建议: {improvementSuggestion}";
        }

        /// <summary>
        /// 发布质量趋势变化事件
        /// </summary>
        private async Task PublishQualityTrendChangedEventAsync(QualityStatistics statistics)
        {
            await _localEventBus.PublishAsync(new QualityTrendChangedEto
            {
                EquipmentId = statistics.EquipmentId,
                QualityTrend = statistics.QualityTrend,
                QualifiedRate = statistics.QualifiedRate,
                StatisticsTime = statistics.StatisticsTime
            });
        }

        /// <summary>
        /// 检查并发布质量预警
        /// </summary>
        private async Task CheckAndPublishQualityAlertAsync(QualityStatistics statistics)
        {
            // 当合格率低于90%时发出预警
            if (statistics.QualifiedRate < 90)
            {
                await _localEventBus.PublishAsync(new QualityAlertEto
                {
                    EquipmentId = statistics.EquipmentId,
                    QualifiedRate = statistics.QualifiedRate,
                    MainDefectType = statistics.MainDefectType,
                    AlertTime = Clock.Now
                });
            }
        }
    }
}