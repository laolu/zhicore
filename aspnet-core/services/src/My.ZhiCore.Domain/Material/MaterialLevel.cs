using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MesNet.Production
{
    /// <summary>
    /// 物料层级关系实体 - 用于管理物料的层级结构
    /// </summary>
    public class MaterialLevel : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 父物料ID
        /// </summary>
        public Guid ParentMaterialId { get; private set; }

        /// <summary>
        /// 子物料ID
        /// </summary>
        public Guid ChildMaterialId { get; private set; }

        /// <summary>
        /// 层级深度（从顶层开始计算，顶层为0）
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 物料路径（用于存储从顶层到当前节点的路径）
        /// </summary>
        public string MaterialPath { get; private set; }

        /// <summary>
        /// 是否为直接父子关系
        /// </summary>
        public bool IsDirectRelation { get; private set; }

        /// <summary>
        /// 关联的BOM ID
        /// </summary>
        public Guid BillOfMaterialId { get; private set; }

        /// <summary>
        /// 用量数量
        /// </summary>
        public decimal Quantity { get; private set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string Unit { get; private set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 是否为替代料
        /// </summary>
        public bool IsAlternative { get; private set; }

        /// <summary>
        /// 替代料优先级（数字越小优先级越高）
        /// </summary>
        public int? AlternativePriority { get; private set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; private set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; private set; }

        protected MaterialLevel() { }

        public MaterialLevel(
            Guid id,
            Guid parentMaterialId,
            Guid childMaterialId,
            int level,
            string materialPath,
            bool isDirectRelation,
            Guid billOfMaterialId,
            decimal quantity,
            string unit,
            string version,
            bool isAlternative = false,
            int? alternativePriority = null,
            DateTime? effectiveDate = null,
            DateTime? expiryDate = null)
        {
            Id = id;
            ParentMaterialId = parentMaterialId;
            ChildMaterialId = childMaterialId;
            Level = level;
            MaterialPath = materialPath;
            IsDirectRelation = isDirectRelation;
            BillOfMaterialId = billOfMaterialId;
            Quantity = quantity;
            Unit = unit;
            Version = version;
            IsAlternative = isAlternative;
            AlternativePriority = alternativePriority;
            EffectiveDate = effectiveDate ?? DateTime.UtcNow;
            ExpiryDate = expiryDate;
        }

        /// <summary>
        /// 检查是否存在循环引用
        /// </summary>
        /// <param name="parentPath">父节点的物料路径</param>
        /// <returns>如果存在循环引用则返回true</returns>
        public bool HasCircularReference(string parentPath)
        {
            return MaterialPath.Contains(parentPath);
        }

        /// <summary>
        /// 更新层级信息
        /// </summary>
        /// <param name="newLevel">新的层级深度</param>
        /// <param name="newPath">新的物料路径</param>
        public void UpdateLevelInfo(int newLevel, string newPath)
        {
            Level = newLevel;
            MaterialPath = newPath;
        }

        /// <summary>
        /// 更新物料用量
        /// </summary>
        /// <param name="quantity">数量</param>
        /// <param name="unit">单位</param>
        public void UpdateQuantity(decimal quantity, string unit)
        {
            Quantity = quantity;
            Unit = unit;
        }

        /// <summary>
        /// 设置为替代料
        /// </summary>
        /// <param name="priority">优先级</param>
        public void SetAsAlternative(int priority)
        {
            IsAlternative = true;
            AlternativePriority = priority;
        }

        /// <summary>
        /// 取消替代料设置
        /// </summary>
        public void UnsetAlternative()
        {
            IsAlternative = false;
            AlternativePriority = null;
        }

        /// <summary>
        /// 更新版本信息
        /// </summary>
        /// <param name="version">新版本号</param>
        public void UpdateVersion(string version)
        {
            Version = version;
        }

        /// <summary>
        /// 更新生效期
        /// </summary>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expiryDate">失效日期</param>
        public void UpdateEffectivePeriod(DateTime effectiveDate, DateTime? expiryDate)
        {
            if (expiryDate.HasValue && effectiveDate > expiryDate.Value)
            {
                throw new ArgumentException("生效日期不能晚于失效日期");
            }

            EffectiveDate = effectiveDate;
            ExpiryDate = expiryDate;
        }

        /// <summary>
        /// 检查当前层级关系是否在有效期内
        /// </summary>
        /// <param name="checkDate">检查日期，默认为当前时间</param>
        /// <returns>如果在有效期内返回true</returns>
        public bool IsEffective(DateTime? checkDate = null)
        {
            var date = checkDate ?? DateTime.UtcNow;
            return date >= EffectiveDate && (!ExpiryDate.HasValue || date <= ExpiryDate.Value);
        }
    }
}