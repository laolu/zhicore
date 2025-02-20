using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料替代料管理实体类，用于定义物料之间的替代关系
    /// </summary>
    public class MaterialSubstitute : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>主物料ID</summary>
        public Guid PrimaryMaterialId { get; private set; }

        /// <summary>替代物料ID</summary>
        public Guid SubstituteMaterialId { get; private set; }

        /// <summary>替代比例</summary>
        public decimal SubstituteRatio { get; private set; }

        /// <summary>优先级（数字越小优先级越高）</summary>
        public int Priority { get; private set; }

        /// <summary>替代规则说明</summary>
        public string SubstituteRule { get; private set; }

        /// <summary>技术要求</summary>
        public string TechnicalRequirements { get; private set; }

        /// <summary>是否需要特殊审批</summary>
        public bool RequiresApproval { get; private set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; private set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpirationDate { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        protected MaterialSubstitute() { }

        public MaterialSubstitute(
            Guid id,
            Guid primaryMaterialId,
            Guid substituteMaterialId,
            decimal substituteRatio,
            int priority,
            string substituteRule,
            string technicalRequirements,
            bool requiresApproval,
            DateTime effectiveDate,
            DateTime? expirationDate = null) : base(id)
        {
            PrimaryMaterialId = primaryMaterialId;
            SubstituteMaterialId = substituteMaterialId;
            SubstituteRatio = substituteRatio;
            Priority = priority;
            SubstituteRule = substituteRule;
            TechnicalRequirements = technicalRequirements;
            RequiresApproval = requiresApproval;
            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
            IsActive = true;
        }

        public void UpdateSubstituteRatio(decimal ratio)
        {
            if (ratio <= 0)
                throw new ArgumentException("Substitute ratio must be greater than zero.", nameof(ratio));

            SubstituteRatio = ratio;
        }

        public void UpdatePriority(int priority)
        {
            if (priority < 0)
                throw new ArgumentException("Priority cannot be negative.", nameof(priority));

            Priority = priority;
        }

        public void UpdateTechnicalRequirements(string requirements)
        {
            TechnicalRequirements = requirements;
        }

        public void UpdateEffectiveDates(DateTime effectiveDate, DateTime? expirationDate)
        {
            if (expirationDate.HasValue && effectiveDate > expirationDate.Value)
                throw new ArgumentException("Effective date cannot be later than expiration date.");

            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}