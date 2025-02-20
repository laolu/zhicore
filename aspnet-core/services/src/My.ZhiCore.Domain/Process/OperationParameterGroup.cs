using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 参数组实体
    /// </summary>
    public class OperationParameterGroup : Entity<Guid>
    {
        /// <summary>
        /// 组编码
        /// </summary>
        public string GroupCode { get; private set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 组描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; private set; }

        protected OperationParameterGroup()
        {
        }

        public OperationParameterGroup(
            Guid id,
            string groupCode,
            string groupName,
            string description)
        {
            Id = id;
            SetGroupCode(groupCode);
            SetGroupName(groupName);
            SetDescription(description);
            SetSortOrder(0);
        }

        private void SetGroupCode(string groupCode)
        {
            if (string.IsNullOrWhiteSpace(groupCode))
            {
                throw new ArgumentException("组编码不能为空", nameof(groupCode));
            }

            if (groupCode.Length > 50)
            {
                throw new ArgumentException("组编码长度不能超过50个字符", nameof(groupCode));
            }

            GroupCode = groupCode;
        }

        private void SetGroupName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("组名称不能为空", nameof(groupName));
            }

            if (groupName.Length > 100)
            {
                throw new ArgumentException("组名称长度不能超过100个字符", nameof(groupName));
            }

            GroupName = groupName;
        }

        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("组描述长度不能超过500个字符", nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// 更新参数组信息
        /// </summary>
        public void Update(
            string groupName,
            string description)
        {
            SetGroupName(groupName);
            SetDescription(description);
            SetSortOrder(0);
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
            {
                throw new ArgumentException("排序号不能小于0", nameof(sortOrder));
            }

            SortOrder = sortOrder;
        }
    }
}