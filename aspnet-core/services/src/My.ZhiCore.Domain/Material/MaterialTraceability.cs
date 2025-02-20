using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料追溯记录实体类，用于记录和追踪物料的完整生命周期信息
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录物料来源信息（供应商、批次、入库等）
    /// - 追踪物料使用记录（领用、消耗、退料等）
    /// - 记录质量检验信息（检验结果、不合格处理等）
    /// - 支持物料状态变更记录
    /// - 记录物料处置信息
    /// </remarks>
    public class MaterialTraceability : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>批次号</summary>
        public string BatchNumber { get; private set; }

        /// <summary>来源类型（采购、退料、调拨等）</summary>
        public string SourceType { get; private set; }

        /// <summary>来源单据编号</summary>
        public string SourceDocumentNumber { get; private set; }

        /// <summary>供应商/来源方</summary>
        public string SourceParty { get; private set; }

        /// <summary>生产日期</summary>
        public DateTime? ManufactureDate { get; private set; }

        /// <summary>有效期至</summary>
        public DateTime? ExpiryDate { get; private set; }

        /// <summary>质量状态（待检、合格、不合格等）</summary>
        public string QualityStatus { get; private set; }

        /// <summary>质检结果</summary>
        public string InspectionResult { get; private set; }

        /// <summary>质检人员</summary>
        public string Inspector { get; private set; }

        /// <summary>质检日期</summary>
        public DateTime? InspectionDate { get; private set; }

        /// <summary>当前状态（在库、已领用、已消耗、已处置等）</summary>
        public string Status { get; private set; }

        /// <summary>数量</summary>
        public decimal Quantity { get; private set; }

        /// <summary>存储位置</summary>
        public string Location { get; private set; }

        /// <summary>备注</summary>
        public string Remarks { get; private set; }

        protected MaterialTraceability() { }

        public MaterialTraceability(
            Guid id,
            Guid materialId,
            string batchNumber,
            string sourceType,
            string sourceDocumentNumber,
            string sourceParty,
            DateTime? manufactureDate,
            DateTime? expiryDate,
            decimal quantity,
            string location) : base(id)
        {
            MaterialId = materialId;
            BatchNumber = batchNumber;
            SourceType = sourceType;
            SourceDocumentNumber = sourceDocumentNumber;
            SourceParty = sourceParty;
            ManufactureDate = manufactureDate;
            ExpiryDate = expiryDate;
            Quantity = quantity;
            Location = location;
            Status = "待检";
            QualityStatus = "待检";
        }

        /// <summary>
        /// 记录质检结果
        /// </summary>
        public void RecordInspection(
            string inspectionResult,
            string inspector,
            DateTime inspectionDate,
            string qualityStatus)
        {
            InspectionResult = inspectionResult;
            Inspector = inspector;
            InspectionDate = inspectionDate;
            QualityStatus = qualityStatus;

            if (qualityStatus == "合格")
            {
                Status = "在库";
            }
            else
            {
                Status = "不合格待处理";
            }
        }

        /// <summary>
        /// 更新物料状态
        /// </summary>
        public void UpdateStatus(string newStatus, string remarks = null)
        {
            Status = newStatus;
            if (!string.IsNullOrEmpty(remarks))
            {
                Remarks = remarks;
            }
        }

        /// <summary>
        /// 更新数量
        /// </summary>
        public void UpdateQuantity(decimal newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(newQuantity));

            Quantity = newQuantity;
        }

        /// <summary>
        /// 更新存储位置
        /// </summary>
        public void UpdateLocation(string newLocation)
        {
            if (string.IsNullOrEmpty(newLocation))
                throw new ArgumentException("Location cannot be empty.", nameof(newLocation));

            Location = newLocation;
        }
    }
}