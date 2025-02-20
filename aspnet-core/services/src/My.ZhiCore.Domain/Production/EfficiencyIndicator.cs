using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 效率指标实体类，用于定义和管理各类效率指标
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 定义和管理效率指标
    /// - 设置指标目标值和警戒值
    /// - 跟踪指标达成情况
    /// - 支持自定义指标
    /// </remarks>
    public class EfficiencyIndicator : Entity<Guid>
    {
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 指标代码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 指标描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 指标类型（产能/质量/时间/成本等）
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; private set; }

        /// <summary>
        /// 目标值
        /// </summary>
        public decimal TargetValue { get; private set; }

        /// <summary>
        /// 警戒值
        /// </summary>
        public decimal WarningValue { get; private set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected EfficiencyIndicator()
        {
        }

        public EfficiencyIndicator(
            Guid id,
            string name,
            string code,
            string description,
            string category,
            string unit,
            decimal targetValue,
            decimal warningValue,
            string formula = null,
            bool isEnabled = true,
            int sortOrder = 0,
            string remarks = null)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            Category = category;
            Unit = unit;
            TargetValue = targetValue;
            WarningValue = warningValue;
            Formula = formula;
            IsEnabled = isEnabled;
            SortOrder = sortOrder;
            Remarks = remarks;
        }
    }
}