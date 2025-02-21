using System;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备分类创建事件
    /// </summary>
    public class EquipmentCategoryCreatedEvent
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父级分类ID
        /// </summary>
        public Guid? ParentId { get; set; }
    }

    /// <summary>
    /// 设备分类更新事件
    /// </summary>
    public class EquipmentCategoryUpdatedEvent
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父级分类ID
        /// </summary>
        public Guid? ParentId { get; set; }
    }

    /// <summary>
    /// 设备分类删除事件
    /// </summary>
    public class EquipmentCategoryDeletedEvent
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}