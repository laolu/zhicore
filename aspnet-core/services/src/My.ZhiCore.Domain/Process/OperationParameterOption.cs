using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 参数选项实体
    /// </summary>
    public class ParameterOption : Entity<Guid>
    {
        /// <summary>
        /// 所属参数Id
        /// </summary>
        public Guid ParameterId { get; private set; }

        /// <summary>
        /// 选项值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 选项标签
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; private set; }

        /// <summary>
        /// 是否默认选项
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// 选项描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrentVersion { get; private set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectiveTime { get; private set; }

        protected ParameterOption()
        {
        }

        public ParameterOption(
            Guid id,
            Guid parameterId,
            string value,
            string label,
            int sortOrder,
            bool isDefault,
            string description,
            int version = 1,
            bool isCurrentVersion = true,
            DateTime? effectiveTime = null)
        {
            Id = id;
            ParameterId = parameterId;
            SetValue(value);
            SetLabel(label);
            SetSortOrder(sortOrder);
            IsDefault = isDefault;
            SetDescription(description);
            Version = version;
            IsCurrentVersion = isCurrentVersion;
            EffectiveTime = effectiveTime ?? DateTime.Now;
        }

        private void SetValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("选项值不能为空", nameof(value));
            }

            if (value.Length > 100)
            {
                throw new ArgumentException("选项值长度不能超过100个字符", nameof(value));
            }

            Value = value;
        }

        private void SetLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException("选项标签不能为空", nameof(label));
            }

            if (label.Length > 100)
            {
                throw new ArgumentException("选项标签长度不能超过100个字符", nameof(label));
            }

            Label = label;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
            {
                throw new ArgumentException("排序号不能小于0", nameof(sortOrder));
            }

            SortOrder = sortOrder;
        }

        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("选项描述长度不能超过500个字符", nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// 更新选项信息
        /// </summary>
        public void Update(
            string value,
            string label,
            int sortOrder,
            bool isDefault,
            string description,
            int version,
            bool isCurrentVersion,
            DateTime? effectiveTime = null)
        {
            SetValue(value);
            SetLabel(label);
            SetSortOrder(sortOrder);
            IsDefault = isDefault;
            SetDescription(description);
            Version = version;
            IsCurrentVersion = isCurrentVersion;
            EffectiveTime = effectiveTime ?? DateTime.Now;
        }
    }
}