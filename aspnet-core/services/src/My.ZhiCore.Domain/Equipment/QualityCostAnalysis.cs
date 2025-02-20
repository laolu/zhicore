using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量成本分析实体类，用于分析和追踪质量相关的成本
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录预防成本（培训、维护等）
    /// - 记录评估成本（检验、测试等）
    /// - 记录失效成本（返工、报废等）
    /// - 分析质量成本构成
    /// - 计算质量成本比率
    /// - 生成成本分析报告
    /// </remarks>
    public class QualityCostAnalysis : Entity<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; private set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime AnalysisTime { get; private set; }

        /// <summary>
        /// 预防成本（元）
        /// </summary>
        public decimal PreventionCost { get; private set; }

        /// <summary>
        /// 评估成本（元）
        /// </summary>
        public decimal AppraisalCost { get; private set; }

        /// <summary>
        /// 内部失效成本（元）
        /// </summary>
        public decimal InternalFailureCost { get; private set; }

        /// <summary>
        /// 外部失效成本（元）
        /// </summary>
        public decimal ExternalFailureCost { get; private set; }

        /// <summary>
        /// 总质量成本（元）
        /// </summary>
        public decimal TotalQualityCost { get; private set; }

        /// <summary>
        /// 质量成本占比（%）
        /// </summary>
        public decimal QualityCostRatio { get; private set; }

        /// <summary>
        /// 成本分析报告
        /// </summary>
        public string CostAnalysisReport { get; private set; }

        /// <summary>
        /// 成本优化建议
        /// </summary>
        public string CostOptimizationSuggestion { get; private set; }

        protected QualityCostAnalysis()
        {
        }

        public QualityCostAnalysis(
            Guid id,
            Guid equipmentId,
            DateTime analysisTime,
            decimal preventionCost,
            decimal appraisalCost,
            decimal internalFailureCost,
            decimal externalFailureCost,
            decimal totalQualityCost,
            decimal qualityCostRatio,
            string costAnalysisReport,
            string costOptimizationSuggestion)
        {
            Id = id;
            EquipmentId = equipmentId;
            AnalysisTime = analysisTime;
            PreventionCost = preventionCost;
            AppraisalCost = appraisalCost;
            InternalFailureCost = internalFailureCost;
            ExternalFailureCost = externalFailureCost;
            TotalQualityCost = totalQualityCost;
            QualityCostRatio = qualityCostRatio;
            CostAnalysisReport = costAnalysisReport;
            CostOptimizationSuggestion = costOptimizationSuggestion;
        }
    }
}