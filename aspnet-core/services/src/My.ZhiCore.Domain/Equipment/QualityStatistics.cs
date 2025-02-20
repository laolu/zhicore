using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 质量统计分析实体类，用于记录和分析质量检验数据
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录质量检验数据（不合格品数量、合格率等）
    /// - 按时间维度统计分析
    /// - 按产品维度统计分析
    /// - 按工序维度统计分析
    /// - 质量趋势分析
    /// - 生成质量报告
    /// </remarks>
    public class QualityStatistics : Entity<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; private set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatisticsTime { get; private set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; private set; }

        /// <summary>
        /// 工序编号
        /// </summary>
        public string ProcessCode { get; private set; }

        /// <summary>
        /// 检验批次号
        /// </summary>
        public string BatchNumber { get; private set; }

        /// <summary>
        /// 检验总数量
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public int DefectCount { get; private set; }

        /// <summary>
        /// 合格率（%）
        /// </summary>
        public decimal QualifiedRate { get; private set; }

        /// <summary>
        /// 主要缺陷类型
        /// </summary>
        public string MainDefectType { get; private set; }

        /// <summary>
        /// 质量趋势（上升/稳定/下降）
        /// </summary>
        public QualityTrend QualityTrend { get; private set; }

        /// <summary>
        /// 质量改进建议
        /// </summary>
        public string ImprovementSuggestion { get; private set; }

        /// <summary>
        /// 统计分析报告
        /// </summary>
        public string AnalysisReport { get; private set; }

        protected QualityStatistics()
        {
        }

        public QualityStatistics(
            Guid id,
            Guid equipmentId,
            DateTime statisticsTime,
            string productCode,
            string processCode,
            string batchNumber,
            int totalCount,
            int defectCount,
            decimal qualifiedRate,
            string mainDefectType,
            QualityTrend qualityTrend,
            string improvementSuggestion,
            string analysisReport)
        {
            Id = id;
            EquipmentId = equipmentId;
            StatisticsTime = statisticsTime;
            ProductCode = productCode;
            ProcessCode = processCode;
            BatchNumber = batchNumber;
            TotalCount = totalCount;
            DefectCount = defectCount;
            QualifiedRate = qualifiedRate;
            MainDefectType = mainDefectType;
            QualityTrend = qualityTrend;
            ImprovementSuggestion = improvementSuggestion;
            AnalysisReport = analysisReport;
        }
    }
}