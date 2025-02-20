using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序质量检验要求实体
    /// </summary>
    public class ProcessStepQualityRequirement : Entity<Guid>
    {
        /// <summary>
        /// 所属工序Id
        /// </summary>
        public Guid ProcessStepId { get; private set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; private set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; private set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; private set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string StandardValue { get; private set; }

        /// <summary>
        /// 允许误差上限
        /// </summary>
        public decimal? UpperTolerance { get; private set; }

        /// <summary>
        /// 允许误差下限
        /// </summary>
        public decimal? LowerTolerance { get; private set; }

        /// <summary>
        /// 检验频率（每N件抽检一次）
        /// </summary>
        public int InspectionFrequency { get; private set; }

        /// <summary>
        /// 是否必检项
        /// </summary>
        public bool IsMandatory { get; private set; }

        /// <summary>
        /// 检验要求描述
        /// </summary>
        public string Description { get; private set; }

        protected ProcessStepQualityRequirement()
        {
        }

        public ProcessStepQualityRequirement(
            Guid id,
            Guid processStepId,
            string inspectionItemName,
            string inspectionItemCode,
            string inspectionMethod,
            string standardValue,
            decimal? upperTolerance,
            decimal? lowerTolerance,
            int inspectionFrequency,
            bool isMandatory,
            string description)
        {
            Id = id;
            ProcessStepId = processStepId;
            SetInspectionItemName(inspectionItemName);
            SetInspectionItemCode(inspectionItemCode);
            SetInspectionMethod(inspectionMethod);
            SetStandardValue(standardValue);
            SetToleranceRange(upperTolerance, lowerTolerance);
            SetInspectionFrequency(inspectionFrequency);
            IsMandatory = isMandatory;
            SetDescription(description);
        }

        private void SetInspectionItemName(string inspectionItemName)
        {
            if (string.IsNullOrWhiteSpace(inspectionItemName))
            {
                throw new ArgumentException("检验项目名称不能为空", nameof(inspectionItemName));
            }

            if (inspectionItemName.Length > 100)
            {
                throw new ArgumentException("检验项目名称长度不能超过100个字符", nameof(inspectionItemName));
            }

            InspectionItemName = inspectionItemName;
        }

        private void SetInspectionItemCode(string inspectionItemCode)
        {
            if (string.IsNullOrWhiteSpace(inspectionItemCode))
            {
                throw new ArgumentException("检验项目代码不能为空", nameof(inspectionItemCode));
            }

            if (inspectionItemCode.Length > 50)
            {
                throw new ArgumentException("检验项目代码长度不能超过50个字符", nameof(inspectionItemCode));
            }

            InspectionItemCode = inspectionItemCode;
        }

        private void SetInspectionMethod(string inspectionMethod)
        {
            if (string.IsNullOrWhiteSpace(inspectionMethod))
            {
                throw new ArgumentException("检验方法不能为空", nameof(inspectionMethod));
            }

            if (inspectionMethod.Length > 500)
            {
                throw new ArgumentException("检验方法长度不能超过500个字符", nameof(inspectionMethod));
            }

            InspectionMethod = inspectionMethod;
        }

        private void SetStandardValue(string standardValue)
        {
            if (string.IsNullOrWhiteSpace(standardValue))
            {
                throw new ArgumentException("标准值不能为空", nameof(standardValue));
            }

            if (standardValue.Length > 100)
            {
                throw new ArgumentException("标准值长度不能超过100个字符", nameof(standardValue));
            }

            StandardValue = standardValue;
        }

        private void SetToleranceRange(decimal? upperTolerance, decimal? lowerTolerance)
        {
            if (upperTolerance.HasValue && lowerTolerance.HasValue && lowerTolerance.Value > upperTolerance.Value)
            {
                throw new ArgumentException("下限不能大于上限");
            }

            UpperTolerance = upperTolerance;
            LowerTolerance = lowerTolerance;
        }

        private void SetInspectionFrequency(int inspectionFrequency)
        {
            if (inspectionFrequency <= 0)
            {
                throw new ArgumentException("检验频率必须大于0", nameof(inspectionFrequency));
            }

            InspectionFrequency = inspectionFrequency;
        }

        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("检验要求描述长度不能超过500个字符", nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// 更新检验要求信息
        /// </summary>
        public void Update(
            string inspectionMethod,
            string standardValue,
            decimal? upperTolerance,
            decimal? lowerTolerance,
            int inspectionFrequency,
            bool isMandatory,
            string description)
        {
            SetInspectionMethod(inspectionMethod);
            SetStandardValue(standardValue);
            SetToleranceRange(upperTolerance, lowerTolerance);
            SetInspectionFrequency(inspectionFrequency);
            IsMandatory = isMandatory;
            SetDescription(description);
        }
    }
}