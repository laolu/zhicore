using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Material.Dtos
{
    /// <summary>
    /// 物料基础DTO
    /// </summary>
    public class MaterialBaseDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        public Dictionary<string, string> TechnicalParameters { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 物料库存DTO
    /// </summary>
    public class MaterialInventoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 安全库存
        /// </summary>
        public decimal SafetyStock { get; set; }

        /// <summary>
        /// 最大库存
        /// </summary>
        public decimal MaxStock { get; set; }

        /// <summary>
        /// 最小库存
        /// </summary>
        public decimal MinStock { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        public string Location { get; set; }
    }

    /// <summary>
    /// 物料质量DTO
    /// </summary>
    public class MaterialQualityDto : EntityDto<Guid>
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 质量标准ID
        /// </summary>
        public Guid QualityStandardId { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; }

        /// <summary>
        /// 质量等级
        /// </summary>
        public string QualityLevel { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime InspectionTime { get; set; }

        /// <summary>
        /// 检验人
        /// </summary>
        public string Inspector { get; set; }

        /// <summary>
        /// 质量参数
        /// </summary>
        public Dictionary<string, string> QualityParameters { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 物料追溯DTO
    /// </summary>
    public class MaterialTraceabilityDto : EntityDto<Guid>
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 追溯编号
        /// </summary>
        public string TraceNumber { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 供应商信息
        /// </summary>
        public string SupplierInfo { get; set; }

        /// <summary>
        /// 流转记录
        /// </summary>
        public List<MaterialFlowRecordDto> FlowRecords { get; set; }
    }

    /// <summary>
    /// 物料流转记录DTO
    /// </summary>
    public class MaterialFlowRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 来源位置
        /// </summary>
        public string SourceLocation { get; set; }

        /// <summary>
        /// 目标位置
        /// </summary>
        public string TargetLocation { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}