using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备分类实体类，用于管理设备的分类层级结构
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 设备分类的基本信息管理（名称、编码、描述）
    /// - 支持多层级分类结构（通过ParentId关联）
    /// - 分类信息的更新维护
    /// </remarks>
    public class EquipmentCategory : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; private set; }
    
        /// <summary>
        /// 分类编码，用于唯一标识分类
        /// </summary>
        public string Code { get; private set; }
    
        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; private set; }
    
        /// <summary>
        /// 父级分类ID，为null时表示顶级分类
        /// </summary>
        public Guid? ParentId { get; private set; }
    
        /// <summary>
        /// 受保护的构造函数，用于ORM
        /// </summary>
        protected EquipmentCategory()
        {
        }
    
        /// <summary>
        /// 创建新的设备分类实例
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <param name="name">分类名称</param>
        /// <param name="code">分类编码</param>
        /// <param name="description">分类描述</param>
        /// <param name="parentId">父级分类ID，可选</param>
        public EquipmentCategory(
            Guid id,
            string name,
            string code,
            string description,
            Guid? parentId = null) : base(id)
        {
            Name = name;
            Code = code;
            Description = description;
            ParentId = parentId;
        }
    
        /// <summary>
        /// 更新设备分类信息
        /// </summary>
        /// <param name="name">新的分类名称</param>
        /// <param name="code">新的分类编码</param>
        /// <param name="description">新的分类描述</param>
        /// <param name="parentId">新的父级分类ID，可选</param>
        public void Update(
            string name,
            string code,
            string description,
            Guid? parentId = null)
        {
            Name = name;
            Code = code;
            Description = description;
            ParentId = parentId;
        }
    }
}