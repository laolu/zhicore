using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料需求预测实体类，用于管理物料的需求预测和智能补货建议
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 基于历史数据的需求预测
    /// - 季节性需求分析
    /// - 智能补货建议
    /// - 预测准确率评估
    /// </remarks>
    public class MaterialDemandForecast : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>预测周期（天）</summary>
        public int ForecastPeriod { get; private set; }

        /// <summary>预测开始日期</summary>
        public DateTime ForecastStartDate { get; private set; }

        /// <summary>预测结束日期</summary>
        public DateTime ForecastEndDate { get; private set; }

        /// <summary>预测需求量</summary>
        public decimal ForecastQuantity { get; private set; }

        /// <summary>实际需求量</summary>
        public decimal ActualQuantity { get; private set; }

        /// <summary>预测准确率</summary>
        public decimal ForecastAccuracy { get; private set; }

        /// <summary>季节性系数</summary>
        public decimal SeasonalityFactor { get; private set; }

        /// <summary>建议补货量</summary>
        public decimal SuggestedReplenishmentQuantity { get; private set; }

        /// <summary>建议补货日期</summary>
        public DateTime? SuggestedReplenishmentDate { get; private set; }

        /// <summary>预测状态（进行中/已完成/已验证）</summary>
        public string Status { get; private set; }

        /// <summary>预测方法</summary>
        public string ForecastMethod { get; private set; }

        /// <summary>备注</summary>
        public string Remarks { get; private set; }

        protected MaterialDemandForecast() { }

        public MaterialDemandForecast(
            Guid id,
            Guid materialId,
            int forecastPeriod,
            DateTime forecastStartDate,
            string forecastMethod) : base(id)
        {
            MaterialId = materialId;
            ForecastPeriod = forecastPeriod;
            ForecastStartDate = forecastStartDate;
            ForecastEndDate = forecastStartDate.AddDays(forecastPeriod);
            ForecastMethod = forecastMethod;
            Status = "进行中";
            ForecastAccuracy = 0;
            SeasonalityFactor = 1;
        }

        /// <summary>
        /// 更新预测结果
        /// </summary>
        public void UpdateForecastResult(
            decimal forecastQuantity,
            decimal seasonalityFactor,
            decimal suggestedReplenishmentQuantity,
            DateTime? suggestedReplenishmentDate)
        {
            ForecastQuantity = forecastQuantity;
            SeasonalityFactor = seasonalityFactor;
            SuggestedReplenishmentQuantity = suggestedReplenishmentQuantity;
            SuggestedReplenishmentDate = suggestedReplenishmentDate;
            Status = "已完成";
        }

        /// <summary>
        /// 验证预测准确性
        /// </summary>
        public void ValidateForecast(decimal actualQuantity)
        {
            ActualQuantity = actualQuantity;
            // 计算预测准确率（1 - 绝对误差率）
            ForecastAccuracy = ForecastQuantity > 0 ?
                1 - Math.Abs(ActualQuantity - ForecastQuantity) / ForecastQuantity :
                0;
            Status = "已验证";
        }

        /// <summary>
        /// 更新备注
        /// </summary>
        public void UpdateRemarks(string remarks)
        {
            Remarks = remarks;
        }
    }
}