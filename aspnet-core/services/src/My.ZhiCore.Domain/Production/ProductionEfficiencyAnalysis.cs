using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产效率分析实体类，用于记录和分析生产效率数据
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录生产线、工序和设备的效率数据
    /// - 支持按时间、产品、工序等维度进行统计分析
    /// - 计算关键效率指标（KPI）
    /// - 跟踪效率趋势变化
    /// - 生成效率分析报告
    /// </remarks>
    public class ProductionEfficiencyAnalysis : Entity<Guid>
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; private set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid? ProcessId { get; private set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid? EquipmentId { get; private set; }

        /// <summary>
        /// 分析时间
        /// </summary>
        public DateTime AnalysisTime { get; private set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public decimal PlannedOutput { get; private set; }

        /// <summary>
        /// 实际产量
        /// </summary>
        public decimal ActualOutput { get; private set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public decimal QualifiedOutput { get; private set; }

        /// <summary>
        /// 计划运行时间（分钟）
        /// </summary>
        public decimal PlannedRuntime { get; private set; }

        /// <summary>
        /// 实际运行时间（分钟）
        /// </summary>
        public decimal ActualRuntime { get; private set; }

        /// <summary>
        /// 停机时间（分钟）
        /// </summary>
        public decimal DownTime { get; private set; }

        /// <summary>
        /// 产能利用率（%）
        /// </summary>
        public decimal CapacityUtilization { get; private set; }

        /// <summary>
        /// 合格率（%）
        /// </summary>
        public decimal QualificationRate { get; private set; }

        /// <summary>
        /// 设备综合效率（OEE）（%）
        /// </summary>
        public decimal OverallEquipmentEffectiveness { get; private set; }

        /// <summary>
        /// 生产节拍（秒/件）
        /// </summary>
        public decimal Takt { get; private set; }

        /// <summary>
        /// 效率趋势（JSON格式存储历史数据）
        /// </summary>
        public string EfficiencyTrend { get; private set; }

        /// <summary>
        /// 效率分析报告
        /// </summary>
        public string AnalysisReport { get; private set; }

        /// <summary>
        /// 改进建议
        /// </summary>
        public string ImprovementSuggestions { get; private set; }

        protected ProductionEfficiencyAnalysis()
        {
        }

        public ProductionEfficiencyAnalysis(
            Guid id,
            Guid productionLineId,
            Guid? processId,
            Guid? equipmentId,
            DateTime analysisTime,
            decimal plannedOutput,
            decimal actualOutput,
            decimal qualifiedOutput,
            decimal plannedRuntime,
            decimal actualRuntime,
            decimal downTime,
            decimal capacityUtilization,
            decimal qualificationRate,
            decimal overallEquipmentEffectiveness,
            decimal takt,
            string efficiencyTrend = null,
            string analysisReport = null,
            string improvementSuggestions = null)
        {
            Id = id;
            ProductionLineId = productionLineId;
            ProcessId = processId;
            EquipmentId = equipmentId;
            AnalysisTime = analysisTime;
            PlannedOutput = plannedOutput;
            ActualOutput = actualOutput;
            QualifiedOutput = qualifiedOutput;
            PlannedRuntime = plannedRuntime;
            ActualRuntime = actualRuntime;
            DownTime = downTime;
            CapacityUtilization = capacityUtilization;
            QualificationRate = qualificationRate;
            OverallEquipmentEffectiveness = overallEquipmentEffectiveness;
            Takt = takt;
            EfficiencyTrend = efficiencyTrend;
            AnalysisReport = analysisReport;
            ImprovementSuggestions = improvementSuggestions;
        }
    }
}