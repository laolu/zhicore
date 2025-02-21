using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量成本分析管理器，负责处理质量成本分析相关的领域逻辑
    /// </summary>
    public class QualityCostAnalysisManager : DomainService
    {
        private readonly IRepository<QualityCostAnalysis, Guid> _qualityCostAnalysisRepository;
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILocalEventBus _localEventBus;

        public QualityCostAnalysisManager(
            IRepository<QualityCostAnalysis, Guid> qualityCostAnalysisRepository,
            IRepository<Equipment, Guid> equipmentRepository,
            ILocalEventBus localEventBus)
        {
            _qualityCostAnalysisRepository = qualityCostAnalysisRepository;
            _equipmentRepository = equipmentRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建质量成本分析记录
        /// </summary>
        public async Task<QualityCostAnalysis> CreateAsync(
            Guid equipmentId,
            decimal preventionCost,
            decimal appraisalCost,
            decimal internalFailureCost,
            decimal externalFailureCost)
        {
            // 验证设备是否存在
            var equipment = await _equipmentRepository.GetAsync(equipmentId);

            // 计算总质量成本和成本比率
            var totalQualityCost = preventionCost + appraisalCost + internalFailureCost + externalFailureCost;
            var qualityCostRatio = (totalQualityCost / equipment.Price) * 100;

            // 生成分析报告和优化建议
            var costAnalysisReport = GenerateCostAnalysisReport(
                preventionCost, appraisalCost, internalFailureCost, externalFailureCost);
            var optimizationSuggestion = GenerateOptimizationSuggestion(
                preventionCost, appraisalCost, internalFailureCost, externalFailureCost);

            var analysis = new QualityCostAnalysis(
                GuidGenerator.Create(),
                equipmentId,
                DateTime.Now,
                preventionCost,
                appraisalCost,
                internalFailureCost,
                externalFailureCost,
                totalQualityCost,
                qualityCostRatio,
                costAnalysisReport,
                optimizationSuggestion
            );

            await _qualityCostAnalysisRepository.InsertAsync(analysis);

            // 发布质量成本分析完成事件
            await _localEventBus.PublishAsync(new QualityCostAnalysisCompletedEventData(
                analysis.Id,
                analysis.EquipmentId,
                analysis.TotalQualityCost,
                analysis.QualityCostRatio
            ));

            return analysis;
        }

        private string GenerateCostAnalysisReport(
            decimal preventionCost,
            decimal appraisalCost,
            decimal internalFailureCost,
            decimal externalFailureCost)
        {
            var totalCost = preventionCost + appraisalCost + internalFailureCost + externalFailureCost;
            return $"质量成本分析报告:\n" +
                   $"1. 预防成本: {preventionCost:C}，占比 {(preventionCost / totalCost) * 100:F2}%\n" +
                   $"2. 评估成本: {appraisalCost:C}，占比 {(appraisalCost / totalCost) * 100:F2}%\n" +
                   $"3. 内部失效成本: {internalFailureCost:C}，占比 {(internalFailureCost / totalCost) * 100:F2}%\n" +
                   $"4. 外部失效成本: {externalFailureCost:C}，占比 {(externalFailureCost / totalCost) * 100:F2}%\n" +
                   $"总成本: {totalCost:C}";
        }

        private string GenerateOptimizationSuggestion(
            decimal preventionCost,
            decimal appraisalCost,
            decimal internalFailureCost,
            decimal externalFailureCost)
        {
            var totalCost = preventionCost + appraisalCost + internalFailureCost + externalFailureCost;
            var suggestions = "成本优化建议:\n";

            if ((preventionCost / totalCost) < 0.2m)
            {
                suggestions += "1. 建议增加预防成本投入，可能通过增加培训或预防性维护来减少失效成本\n";
            }

            if ((internalFailureCost + externalFailureCost) / totalCost > 0.4m)
            {
                suggestions += "2. 失效成本占比过高，建议加强质量控制和预防措施\n";
            }

            if (externalFailureCost > internalFailureCost)
            {
                suggestions += "3. 外部失效成本高于内部失效成本，建议加强出厂检验力度\n";
            }

            return suggestions;
        }
    }

    /// <summary>
    /// 质量成本分析完成事件数据
    /// </summary>
    public class QualityCostAnalysisCompletedEventData
    {
        public Guid AnalysisId { get; }
        public Guid EquipmentId { get; }
        public decimal TotalQualityCost { get; }
        public decimal QualityCostRatio { get; }

        public QualityCostAnalysisCompletedEventData(
            Guid analysisId,
            Guid equipmentId,
            decimal totalQualityCost,
            decimal qualityCostRatio)
        {
            AnalysisId = analysisId;
            EquipmentId = equipmentId;
            TotalQualityCost = totalQualityCost;
            QualityCostRatio = qualityCostRatio;
        }
    }
}