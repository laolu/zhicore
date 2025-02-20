using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料质量标准实体类，用于定义物料的质量要求和检验标准
    /// </summary>
    public class MaterialQualityStandard : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>质量标准编号</summary>
        public string StandardCode { get; private set; }

        /// <summary>质量标准名称</summary>
        public string StandardName { get; private set; }

        /// <summary>技术要求</summary>
        public string TechnicalRequirements { get; private set; }

        /// <summary>检验方法</summary>
        public string InspectionMethod { get; private set; }

        /// <summary>允许误差范围</summary>
        public string Tolerance { get; private set; }

        /// <summary>质量等级</summary>
        public string QualityLevel { get; private set; }

        /// <summary>检验周期（天）</summary>
        public int InspectionCycle { get; private set; }

        /// <summary>是否需要质量证书</summary>
        public bool RequiresCertificate { get; private set; }

        /// <summary>特殊存储要求</summary>
        public string StorageRequirements { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        protected MaterialQualityStandard() { }

        public MaterialQualityStandard(
            Guid id,
            Guid materialId,
            string standardCode,
            string standardName,
            string technicalRequirements,
            string inspectionMethod,
            string tolerance,
            string qualityLevel,
            int inspectionCycle,
            bool requiresCertificate,
            string storageRequirements) : base(id)
        {
            MaterialId = materialId;
            StandardCode = standardCode;
            StandardName = standardName;
            TechnicalRequirements = technicalRequirements;
            InspectionMethod = inspectionMethod;
            Tolerance = tolerance;
            QualityLevel = qualityLevel;
            InspectionCycle = inspectionCycle;
            RequiresCertificate = requiresCertificate;
            StorageRequirements = storageRequirements;
            IsActive = true;
        }

        public void UpdateTechnicalRequirements(string requirements)
        {
            TechnicalRequirements = requirements;
        }

        public void UpdateInspectionMethod(string method)
        {
            InspectionMethod = method;
        }

        public void UpdateInspectionCycle(int cycle)
        {
            if (cycle <= 0)
                throw new ArgumentException("Inspection cycle must be greater than zero.", nameof(cycle));

            InspectionCycle = cycle;
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