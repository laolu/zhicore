using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// 物料清单（BOM）实体，用于管理产品的物料结构和组成关系
    /// </summary>
    public class BillOfMaterial : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>物料清单编码</summary>
        public string Code { get; private set; }
        /// <summary>物料清单名称</summary>
        public string Name { get; private set; }

        /// <summary>物料清单描述</summary>
        public string Description { get; private set; }

        /// <summary>物料清单版本号</summary>
        public string Version { get; private set; }

        /// <summary>BOM类型</summary>
        public BomType Type { get; private set; }

        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; private set; }

        /// <summary>失效日期（可选）</summary>
        public DateTime? ExpirationDate { get; private set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; private set; }

        /// <summary>变更说明</summary>
        public string ChangeDescription { get; private set; }

        /// <summary>前一版本ID</summary>
        public Guid? PreviousVersionId { get; private set; }

        /// <summary>
        /// 记录变更历史
        /// </summary>
        /// <param name="reason">变更原因</param>
        /// <param name="description">变更说明</param>
        /// <param name="previousVersionId">前一版本ID</param>
        public void RecordChange(string reason, string description, Guid? previousVersionId = null)
        {
            ChangeReason = reason;
            ChangeDescription = description;
            PreviousVersionId = previousVersionId;
        }

        /// <summary>
        /// 清除变更记录
        /// </summary>
        public void ClearChangeRecord()
        {
            ChangeReason = null;
            ChangeDescription = null;
            PreviousVersionId = null;
        }

        /// <summary>
        /// 保护的构造函数，供ORM使用
        /// </summary>
        protected BillOfMaterial() { }

        /// <summary>
        /// 创建物料清单
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="code">物料清单编码</param>
        /// <param name="name">物料清单名称</param>
        /// <param name="description">物料清单描述</param>
        /// <param name="version">版本号</param>
        /// <param name="type">BOM类型</param>
        /// <param name="materialId">关联的物料ID</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期（可选）</param>
        public BillOfMaterial(
            Guid id,
            string code,
            string name,
            string description,
            string version,
            BomType type,
            Guid materialId,
            DateTime effectiveDate,
            DateTime? expirationDate = null) : base(id)
        {
            Code = code;
            Name = name;
            Description = description;
            Version = version;
            Type = type;
            MaterialId = materialId;
            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
            IsActive = true;
        }

        /// <summary>
        /// 启用物料清单
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// 禁用物料清单
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// 更新物料清单版本
        /// </summary>
        /// <param name="newVersion">新版本号</param>
        public void UpdateVersion(string newVersion)
        {
            Version = newVersion;
        }

        /// <summary>
        /// 更新物料清单的生效时间
        /// </summary>
        /// <param name="effectiveDate">新的生效日期</param>
        /// <param name="expirationDate">新的失效日期（可选）</param>
        public void UpdateEffectiveDates(DateTime effectiveDate, DateTime? expirationDate)
        {
            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
        }
    }
}