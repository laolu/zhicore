using System;
using System.Collections.Generic;

namespace My.ZhiCore.Material.Dtos
{
    /// <summary>
    /// 创建物料DTO
    /// </summary>
    public class CreateMaterialDto
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
    /// 更新物料DTO
    /// </summary>
    public class UpdateMaterialDto
    {
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
    /// 创建物料库存DTO
    /// </summary>
    public class CreateMaterialInventoryDto
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
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        public string Location { get; set; }
    }

    /// <summary>
    /// 更新物料库存DTO
    /// </summary>
    public class UpdateMaterialInventoryDto
    {
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
        /// 位置信息
        /// </summary>
        public string Location { get; set; }
    }

    /// <summary>
    /// 创建物料质量DTO
    /// </summary>
    public class CreateMaterialQualityDto
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
    /// 更新物料质量DTO
    /// </summary>
    public class UpdateMaterialQualityDto
    {
        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; }

        /// <summary>
        /// 质量等级
        /// </summary>
        public string QualityLevel { get; set; }

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
    /// 创建物料追溯DTO
    /// </summary>
    public class CreateMaterialTraceabilityDto
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
    }

    /// <summary>
    /// 更新物料追溯DTO
    /// </summary>
    public class UpdateMaterialTraceabilityDto
    {
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 供应商信息
        /// </summary>
        public string SupplierInfo { get; set; }
    }
}