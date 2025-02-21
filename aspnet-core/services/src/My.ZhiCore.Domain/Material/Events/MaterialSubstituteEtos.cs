using System;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料替代料创建事件
    /// </summary>
    public class MaterialSubstituteCreatedEto
    {
        public Guid Id { get; set; }
        public Guid PrimaryMaterialId { get; set; }
        public Guid SubstituteMaterialId { get; set; }
    }

    /// <summary>
    /// 物料替代料更新事件
    /// </summary>
    public class MaterialSubstituteUpdatedEto
    {
        public Guid Id { get; set; }
        public decimal SubstituteRatio { get; set; }
    }

    /// <summary>
    /// 物料替代料有效期更新事件
    /// </summary>
    public class MaterialSubstituteDatesUpdatedEto
    {
        public Guid Id { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    /// <summary>
    /// 物料替代料状态变更事件
    /// </summary>
    public class MaterialSubstituteStatusChangedEto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}