using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 参数类型枚举
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// 文本型
        /// </summary>
        Text = 0,
    
        /// <summary>
        /// 数值型
        /// </summary>
        Numeric = 1,
    
        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean = 2,
    
        /// <summary>
        /// 日期型
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// 选项型
        /// </summary>
        Option = 4
    }
    
    /// <summary>
    /// 工序参数实体
    /// </summary>
    public class ProcessStepParameter : Entity<Guid>
    {
        /// <summary>
        /// 所属工序Id
        /// </summary>
        public Guid ProcessStepId { get; private set; }

        /// <summary>
        /// 所属参数组Id
        /// </summary>
        public Guid? GroupId { get; private set; }
    
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; private set; }
    
        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParameterCode { get; private set; }
    
        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterType ParameterType { get; private set; }
    
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; private set; }
    
        /// <summary>
        /// 参数单位
        /// </summary>
        public string Unit { get; private set; }
    
        /// <summary>
        /// 最小值
        /// </summary>
        public decimal? MinValue { get; private set; }
    
        /// <summary>
        /// 最大值
        /// </summary>
        public decimal? MaxValue { get; private set; }
    
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; private set; }
    
        /// <summary>
        /// 参数描述
        /// </summary>
        public string Description { get; private set; }
    
        protected ProcessStepParameter()
        {
        }
    
        public ProcessStepParameter(
            Guid id,
            Guid processStepId,
            Guid? groupId,
            string parameterName,
            string parameterCode,
            ParameterType parameterType,
            string value,
            string unit,
            decimal? minValue,
            decimal? maxValue,
            bool isRequired,
            string description)
        {
            Id = id;
            ProcessStepId = processStepId;
            GroupId = groupId;
            SetParameterName(parameterName);
            SetParameterCode(parameterCode);
            SetParameterType(parameterType);
            SetValue(value);
            SetUnit(unit);
            SetValueRange(minValue, maxValue);
            IsRequired = isRequired;
            SetDescription(description);
        }
    
        private void SetParameterName(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException("参数名称不能为空", nameof(parameterName));
            }
    
            if (parameterName.Length > 100)
            {
                throw new ArgumentException("参数名称长度不能超过100个字符", nameof(parameterName));
            }
    
            ParameterName = parameterName;
        }
    
        private void SetParameterCode(string parameterCode)
        {
            if (string.IsNullOrWhiteSpace(parameterCode))
            {
                throw new ArgumentException("参数代码不能为空", nameof(parameterCode));
            }
    
            if (parameterCode.Length > 50)
            {
                throw new ArgumentException("参数代码长度不能超过50个字符", nameof(parameterCode));
            }
    
            ParameterCode = parameterCode;
        }
    
        private void SetParameterType(ParameterType parameterType)
        {
            if (!Enum.IsDefined(typeof(ParameterType), parameterType))
            {
                throw new ArgumentException("无效的参数类型", nameof(parameterType));
            }
    
            ParameterType = parameterType;
        }
    
        private void SetValue(string value)
        {
            if (IsRequired && string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("必填参数的值不能为空", nameof(value));
            }
    
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > 500)
                {
                    throw new ArgumentException("参数值长度不能超过500个字符", nameof(value));
                }
    
                // 根据参数类型验证值的格式
                switch (ParameterType)
                {
                    case ParameterType.Numeric:
                        if (!decimal.TryParse(value, out var numericValue))
                        {
                            throw new ArgumentException("数值型参数的值必须是有效的数字", nameof(value));
                        }
                        // 验证数值范围
                        if (MinValue.HasValue && numericValue < MinValue.Value)
                        {
                            throw new ArgumentException($"参数值不能小于最小值{MinValue.Value}", nameof(value));
                        }
                        if (MaxValue.HasValue && numericValue > MaxValue.Value)
                        {
                            throw new ArgumentException($"参数值不能大于最大值{MaxValue.Value}", nameof(value));
                        }
                        break;
                    case ParameterType.Boolean:
                        if (!bool.TryParse(value, out _))
                        {
                            throw new ArgumentException("布尔型参数的值必须是true或false", nameof(value));
                        }
                        break;
                    case ParameterType.DateTime:
                        if (!DateTime.TryParse(value, out _))
                        {
                            throw new ArgumentException("日期型参数的值必须是有效的日期时间格式", nameof(value));
                        }
                        break;
                    case ParameterType.Option:
                        // 选项型参数的值验证将在领域服务层进行
                        break;
                }
            }
    
            Value = value;
        }
    
        private void SetUnit(string unit)
        {
            if (!string.IsNullOrEmpty(unit) && unit.Length > 20)
            {
                throw new ArgumentException("参数单位长度不能超过20个字符", nameof(unit));
            }
    
            Unit = unit;
        }
    
        private void SetValueRange(decimal? minValue, decimal? maxValue)
        {
            if (minValue.HasValue && maxValue.HasValue && minValue.Value > maxValue.Value)
            {
                throw new ArgumentException("最小值不能大于最大值");
            }
    
            // 只有数值型参数才能设置取值范围
            if ((minValue.HasValue || maxValue.HasValue) && ParameterType != ParameterType.Numeric)
            {
                throw new ArgumentException("只有数值型参数才能设置取值范围");
            }
    
            MinValue = minValue;
            MaxValue = maxValue;
        }
    
        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("参数描述长度不能超过500个字符", nameof(description));
            }
    
            Description = description;
        }
    
        /// <summary>
        /// 更新参数信息
        /// </summary>
        public void Update(
            string value,
            string unit,
            decimal? minValue,
            decimal? maxValue,
            bool isRequired,
            string description)
        {
            SetValue(value);
            SetUnit(unit);
            SetValueRange(minValue, maxValue);
            IsRequired = isRequired;
            SetDescription(description);
        }
    }
}