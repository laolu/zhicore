using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料分类实体类，用于管理物料的分类层级结构
    /// </summary>
    public class MaterialCategory : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>分类名称</summary>
        public string Name { get; private set; }

        /// <summary>分类编码</summary>
        public string Code { get; private set; }

        /// <summary>父分类ID</summary>
        public Guid? ParentId { get; private set; }

        /// <summary>分类层级路径，使用"/"分隔的ID路径</summary>
        public string Path { get; private set; }

        /// <summary>分类层级深度，从0开始</summary>
        public int Level { get; private set; }

        /// <summary>分类描述</summary>
        public string Description { get; private set; }

        /// <summary>排序号</summary>
        public int SortOrder { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        /// <summary>属性模板ID</summary>
        public Guid? AttributeTemplateId { get; private set; }

        protected MaterialCategory() { }

        public MaterialCategory(
            Guid id,
            string name,
            string code,
            Guid? parentId,
            string path,
            string description,
            int sortOrder,
            Guid? attributeTemplateId) : base(id)
        {
            Name = name;
            Code = code;
            ParentId = parentId;
            Path = path;
            Level = path.Split('/').Length - 1;
            Description = description;
            SortOrder = sortOrder;
            IsActive = true;
            AttributeTemplateId = attributeTemplateId;
        }

        /// <summary>
        /// 更新分类信息
        /// </summary>
        public void Update(
            string name,
            string code,
            string description,
            int sortOrder,
            Guid? attributeTemplateId)
        {
            Name = name;
            Code = code;
            Description = description;
            SortOrder = sortOrder;
            AttributeTemplateId = attributeTemplateId;
        }

        /// <summary>
        /// 更新分类状态
        /// </summary>
        public void UpdateStatus(bool isActive)
        {
            IsActive = isActive;
        }

        /// <summary>
        /// 更新分类路径
        /// </summary>
        public void UpdatePath(string newPath)
        {
            Path = newPath;
            Level = newPath.Split('/').Length - 1;
        }
    }
}