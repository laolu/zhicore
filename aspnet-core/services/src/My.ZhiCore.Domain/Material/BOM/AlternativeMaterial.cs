using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// 替代物料实体，用于管理物料之间的替代关系
    /// </summary>
    public class AlternativeMaterial : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>主物料ID</summary>
        public Guid PrimaryMaterialId { get; private set; }

        /// <summary>替代物料ID</summary>
        public Guid SubstituteMaterialId { get; private set; }

        /// <summary>转换比率（替代物料与主物料的数量转换关系）</summary>
        public decimal ConversionRate { get; private set; }

        /// <summary>替代说明</summary>
        public string Description { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        /// <summary>优先级（数字越小优先级越高）</summary>
        public int Priority { get; private set; }

        /// <summary>
        /// 保护的构造函数，供ORM使用
        /// </summary>
        protected AlternativeMaterial() { }

        /// <summary>
        /// 创建替代物料关系
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="primaryMaterialId">主物料ID</param>
        /// <param name="substituteMaterialId">替代物料ID</param>
        /// <param name="conversionRate">转换比率（必须大于0）</param>
        /// <param name="description">替代说明</param>
        /// <param name="priority">优先级</param>
        public AlternativeMaterial(
            Guid id,
            Guid primaryMaterialId,
            Guid substituteMaterialId,
            decimal conversionRate,
            string description,
            int priority) : base(id)
        {
            if (conversionRate <= 0)
                throw new ArgumentException("Conversion rate must be greater than zero.", nameof(conversionRate));

            PrimaryMaterialId = primaryMaterialId;
            SubstituteMaterialId = substituteMaterialId;
            ConversionRate = conversionRate;
            Description = description;
            Priority = priority;
            IsActive = true;
        }

        /// <summary>
        /// 更新转换比率
        /// </summary>
        /// <param name="newRate">新的转换比率（必须大于0）</param>
        /// <exception cref="ArgumentException">当转换比率小于或等于0时抛出</exception>
        public void UpdateConversionRate(decimal newRate)
        {
            if (newRate <= 0)
                throw new ArgumentException("Conversion rate must be greater than zero.", nameof(newRate));

            ConversionRate = newRate;
        }

        /// <summary>
        /// 更新替代说明
        /// </summary>
        /// <param name="newDescription">新的替代说明</param>
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        /// <summary>
        /// 更新优先级
        /// </summary>
        /// <param name="newPriority">新的优先级</param>
        public void UpdatePriority(int newPriority)
        {
            Priority = newPriority;
        }

        /// <summary>
        /// 启用替代关系
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// 禁用替代关系
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}