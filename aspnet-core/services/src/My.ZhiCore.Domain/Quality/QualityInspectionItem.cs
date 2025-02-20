using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace MesNet.Quality
{
    /// <summary>
    /// 质量检验项目实体
    /// </summary>
    public class QualityInspectionItem : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 检验项目编号
        /// </summary>
        public string ItemNumber { get; private set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 检验项目描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public InspectionType Type { get; private set; }

        /// <summary>
        /// 检验标准值
        /// </summary>
        public decimal? StandardValue { get; private set; }

        /// <summary>
        /// 上限值
        /// </summary>
        public decimal? UpperLimit { get; private set; }

        /// <summary>
        /// 下限值
        /// </summary>
        public decimal? LowerLimit { get; private set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; private set; }

        /// <summary>
        /// 检验频率（小时）
        /// </summary>
        public decimal InspectionFrequency { get; private set; }

        /// <summary>
        /// 严重程度
        /// </summary>
        public SeverityLevel SeverityLevel { get; private set; }

        /// <summary>
        /// 是否为必检项
        /// </summary>
        public bool IsMandatory { get; private set; }

        /// <summary>
        /// 检验结果记录
        /// </summary>
        public ICollection<InspectionResult> Results { get; private set; }

        /// <summary>
        /// 质量问题记录
        /// </summary>
        public ICollection<QualityIssue> Issues { get; private set; }

        protected QualityInspectionItem()
        {
            Results = new List<InspectionResult>();
            Issues = new List<QualityIssue>();
        }

        public QualityInspectionItem(
            Guid id,
            string itemNumber,
            string name,
            InspectionType type,
            string inspectionMethod,
            decimal inspectionFrequency,
            SeverityLevel severityLevel,
            bool isMandatory = true,
            string description = null,
            decimal? standardValue = null,
            decimal? upperLimit = null,
            decimal? lowerLimit = null)
        {
            Id = id;
            ItemNumber = itemNumber;
            Name = name;
            Type = type;
            InspectionMethod = inspectionMethod;
            InspectionFrequency = inspectionFrequency;
            SeverityLevel = severityLevel;
            IsMandatory = isMandatory;
            Description = description;
            StandardValue = standardValue;
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
            Results = new List<InspectionResult>();
            Issues = new List<QualityIssue>();

            ValidateValues();
        }

        /// <summary>
        /// 添加检验结果
        /// </summary>
        public void AddInspectionResult(object value, string inspector, string remark = null)
        {
            if (!ValidateValue(value))
                throw new ArgumentException("检验结果不符合要求");

            var result = new InspectionResult(Guid.NewGuid(), Id, value, inspector, remark);
            Results.Add(result);

            // 检查是否需要创建质量问题
            if (!IsValueWithinLimits(value))
            {
                var issue = new QualityIssue(
                    Guid.NewGuid(),
                    Id,
                    result.Id,
                    $"{Name}检验结果超出限制范围",
                    SeverityLevel);
                Issues.Add(issue);
            }
        }

        /// <summary>
        /// 更新检验标准
        /// </summary>
        public void UpdateStandard(
            decimal? standardValue,
            decimal? upperLimit,
            decimal? lowerLimit)
        {
            StandardValue = standardValue;
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;

            ValidateValues();
        }

        /// <summary>
        /// 验证检验结果是否在允许范围内
        /// </summary>
        private bool ValidateValue(object value)
        {
            if (value == null && IsMandatory)
                return false;

            if (value == null && !IsMandatory)
                return true;

            switch (Type)
            {
                case InspectionType.Numeric:
                    return value is decimal;

                case InspectionType.Boolean:
                    return value is bool;

                case InspectionType.Text:
                    return value is string;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 验证数值是否在限制范围内
        /// </summary>
        private bool IsValueWithinLimits(object value)
        {
            if (Type != InspectionType.Numeric || value is not decimal numericValue)
                return true;

            if (UpperLimit.HasValue && numericValue > UpperLimit.Value)
                return false;

            if (LowerLimit.HasValue && numericValue < LowerLimit.Value)
                return false;

            return true;
        }

        /// <summary>
        /// 验证标准值和限制范围的有效性
        /// </summary>
        private void ValidateValues()
        {
            if (Type != InspectionType.Numeric)
                return;

            if (UpperLimit.HasValue && LowerLimit.HasValue && UpperLimit.Value < LowerLimit.Value)
                throw new ArgumentException("上限值不能小于下限值");

            if (StandardValue.HasValue)
            {
                if (UpperLimit.HasValue && StandardValue.Value > UpperLimit.Value)
                    throw new ArgumentException("标准值不能大于上限值");

                if (LowerLimit.HasValue && StandardValue.Value < LowerLimit.Value)
                    throw new ArgumentException("标准值不能小于下限值");
            }
        }
    }
}