using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 参数组层级实体
    /// </summary>
    public class ParameterGroupHierarchy : Entity<Guid>
    {
        /// <summary>
        /// 父级参数组Id
        /// </summary>
        public Guid? ParentGroupId { get; private set; }

        /// <summary>
        /// 子级参数组Id
        /// </summary>
        public Guid ChildGroupId { get; private set; }

        /// <summary>
        /// 层级深度
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; private set; }

        protected ParameterGroupHierarchy()
        {
        }

        public ParameterGroupHierarchy(
            Guid id,
            Guid? parentGroupId,
            Guid childGroupId,
            int level,
            int sortOrder)
        {
            Id = id;
            ParentGroupId = parentGroupId;
            SetChildGroupId(childGroupId);
            SetLevel(level);
            SetSortOrder(sortOrder);
        }

        private void SetChildGroupId(Guid childGroupId)
        {
            if (childGroupId == Guid.Empty)
            {
                throw new ArgumentException("子级参数组Id不能为空", nameof(childGroupId));
            }

            if (childGroupId == ParentGroupId)
            {
                throw new ArgumentException("子级参数组不能与父级参数组相同", nameof(childGroupId));
            }

            ChildGroupId = childGroupId;
        }

        private void SetLevel(int level)
        {
            if (level < 1)
            {
                throw new ArgumentException("层级深度不能小于1", nameof(level));
            }

            if (level > 5)
            {
                throw new ArgumentException("层级深度不能大于5", nameof(level));
            }

            Level = level;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
            {
                throw new ArgumentException("排序号不能小于0", nameof(sortOrder));
            }

            SortOrder = sortOrder;
        }

        /// <summary>
        /// 更新层级信息
        /// </summary>
        public void Update(
            Guid? parentGroupId,
            int sortOrder)
        {
            ParentGroupId = parentGroupId;
            SetSortOrder(sortOrder);
        }
    }
}}