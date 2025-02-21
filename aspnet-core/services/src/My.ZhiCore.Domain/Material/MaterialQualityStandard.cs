using System;
using System.Collections.Generic;
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

        /// <summary>版本号</summary>
        public string Version { get; private set; }

        /// <summary>审核状态</summary>
        public QualityStandardStatus Status { get; private set; }

        /// <summary>审核意见</summary>
        public string AuditComments { get; private set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; private set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpirationDate { get; private set; }

        /// <summary>变更历史记录</summary>
        public List<QualityStandardChangeRecord> ChangeHistory { get; private set; }

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
            ValidateConstructorParameters(materialId, standardCode, standardName, inspectionCycle);

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
            IsActive = false;
            Version = "1.0.0";
            Status = QualityStandardStatus.Draft;
            ChangeHistory = new List<QualityStandardChangeRecord>();
        }

        private void ValidateConstructorParameters(Guid materialId, string standardCode, string standardName, int inspectionCycle)
        {
            if (materialId == Guid.Empty)
                throw new ArgumentException("Material ID cannot be empty.", nameof(materialId));

            if (string.IsNullOrWhiteSpace(standardCode))
                throw new ArgumentException("Standard code cannot be empty.", nameof(standardCode));

            if (string.IsNullOrWhiteSpace(standardName))
                throw new ArgumentException("Standard name cannot be empty.", nameof(standardName));

            if (inspectionCycle <= 0)
                throw new ArgumentException("Inspection cycle must be greater than zero.", nameof(inspectionCycle));
        }

        public void UpdateTechnicalRequirements(string requirements)
        {
            if (Status != QualityStandardStatus.Draft)
                throw new InvalidOperationException("只能在草稿状态下修改技术要求");

            TechnicalRequirements = requirements;
            AddChangeRecord("更新技术要求");
        }

        public void UpdateInspectionMethod(string method)
        {
            if (Status != QualityStandardStatus.Draft)
                throw new InvalidOperationException("只能在草稿状态下修改检验方法");

            InspectionMethod = method;
            AddChangeRecord("更新检验方法");
        }

        public void UpdateInspectionCycle(int cycle)
        {
            if (Status != QualityStandardStatus.Draft)
                throw new InvalidOperationException("只能在草稿状态下修改检验周期");

            if (cycle <= 0)
                throw new ArgumentException("Inspection cycle must be greater than zero.", nameof(cycle));

            InspectionCycle = cycle;
            AddChangeRecord("更新检验周期");
        }

        public void SubmitForReview()
        {
            if (Status != QualityStandardStatus.Draft)
                throw new InvalidOperationException("只能提交草稿状态的质量标准进行审核");

            Status = QualityStandardStatus.PendingReview;
            AddChangeRecord("提交审核");
        }

        public void Approve(string comments, DateTime effectiveDate, DateTime? expirationDate = null)
        {
            if (Status != QualityStandardStatus.PendingReview)
                throw new InvalidOperationException("只能审核待审核状态的质量标准");

            if (effectiveDate < DateTime.Now)
                throw new ArgumentException("生效日期不能早于当前日期", nameof(effectiveDate));

            if (expirationDate.HasValue && expirationDate.Value <= effectiveDate)
                throw new ArgumentException("失效日期必须晚于生效日期", nameof(expirationDate));

            Status = QualityStandardStatus.Approved;
            AuditComments = comments;
            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
            IsActive = true;
            AddChangeRecord("审核通过");
        }

        public void Reject(string comments)
        {
            if (Status != QualityStandardStatus.PendingReview)
                throw new InvalidOperationException("只能驳回待审核状态的质量标准");

            Status = QualityStandardStatus.Rejected;
            AuditComments = comments;
            AddChangeRecord("审核驳回");
        }

        public void CreateNewVersion()
        {
            if (Status != QualityStandardStatus.Approved)
                throw new InvalidOperationException("只能基于已审核通过的质量标准创建新版本");

            var versionParts = Version.Split('.');
            var majorVersion = int.Parse(versionParts[0]);
            Version = $"{majorVersion + 1}.0.0";
            Status = QualityStandardStatus.Draft;
            AuditComments = null;
            EffectiveDate = null;
            ExpirationDate = null;
            IsActive = false;
            AddChangeRecord("创建新版本");
        }

        private void AddChangeRecord(string changeType)
        {
            ChangeHistory.Add(new QualityStandardChangeRecord
            {
                ChangeType = changeType,
                Version = Version,
                ChangeTime = DateTime.Now
            });
        }

        public void Activate()
        {
            if (Status != QualityStandardStatus.Approved)
                throw new InvalidOperationException("只能启用已审核通过的质量标准");

            IsActive = true;
            AddChangeRecord("启用质量标准");
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            AddChangeRecord("停用质量标准");
        }
    }

    public enum QualityStandardStatus
    {
        /// <summary>草稿</summary>
        Draft = 0,

        /// <summary>待审核</summary>
        PendingReview = 1,

        /// <summary>已审核</summary>
        Approved = 2,

        /// <summary>已驳回</summary>
        Rejected = 3
    }

    public class QualityStandardChangeRecord
    {
        /// <summary>变更类型</summary>
        public string ChangeType { get; set; }

        /// <summary>变更时的版本号</summary>
        public string Version { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }
    }
}