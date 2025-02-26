using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料成本DTO
    /// </summary>
    public class MaterialCostDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 成本类型（原材料成本、加工成本、运输成本等）
        /// </summary>
        public string CostType { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 计算日期
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建物料成本DTO
    /// </summary>
    public class CreateMaterialCostDto
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 成本类型
        /// </summary>
        public string CostType { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 计算日期
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新物料成本DTO
    /// </summary>
    public class UpdateMaterialCostDto
    {
        /// <summary>
        /// 成本类型
        /// </summary>
        public string CostType { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 计算日期
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 物料替代DTO
    /// </summary>
    public class MaterialSubstituteDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 原物料ID
        /// </summary>
        public Guid OriginalMaterialId { get; set; }

        /// <summary>
        /// 替代物料ID
        /// </summary>
        public Guid SubstituteMaterialId { get; set; }

        /// <summary>
        /// 替代比例
        /// </summary>
        public decimal SubstituteRatio { get; set; }

        /// <summary>
        /// 替代原因
        /// </summary>
        public string SubstituteReason { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 创建物料替代DTO
    /// </summary>
    public class CreateMaterialSubstituteDto
    {
        /// <summary>
        /// 原物料ID
        /// </summary>
        public Guid OriginalMaterialId { get; set; }

        /// <summary>
        /// 替代物料ID
        /// </summary>
        public Guid SubstituteMaterialId { get; set; }

        /// <summary>
        /// 替代比例
        /// </summary>
        public decimal SubstituteRatio { get; set; }

        /// <summary>
        /// 替代原因
        /// </summary>
        public string SubstituteReason { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 更新物料替代DTO
    /// </summary>
    public class UpdateMaterialSubstituteDto
    {
        /// <summary>
        /// 替代比例
        /// </summary>
        public decimal SubstituteRatio { get; set; }

        /// <summary>
        /// 替代原因
        /// </summary>
        public string SubstituteReason { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 物料预警DTO
    /// </summary>
    public class MaterialAlertDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 预警类型（库存不足、价格波动、质量问题等）
        /// </summary>
        public string AlertType { get; set; }

        /// <summary>
        /// 预警级别（低、中、高）
        /// </summary>
        public string AlertLevel { get; set; }

        /// <summary>
        /// 预警阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public decimal CurrentValue { get; set; }

        /// <summary>
        /// 预警消息
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// 预警时间
        /// </summary>
        public DateTime AlertTime { get; set; }

        /// <summary>
        /// 处理状态（未处理、处理中、已处理）
        /// </summary>
        public string ProcessStatus { get; set; }

        /// <summary>
        /// 处理备注
        /// </summary>
        public string ProcessRemarks { get; set; }
    }

    /// <summary>
    /// 创建物料预警DTO
    /// </summary>
    public class CreateMaterialAlertDto
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 预警类型
        /// </summary>
        public string AlertType { get; set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        public string AlertLevel { get; set; }

        /// <summary>
        /// 预警阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public decimal CurrentValue { get; set; }

        /// <summary>
        /// 预警消息
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// 预警时间
        /// </summary>
        public DateTime AlertTime { get; set; }
    }

    /// <summary>
    /// 更新物料预警DTO
    /// </summary>
    public class UpdateMaterialAlertDto
    {
        /// <summary>
        /// 预警级别
        /// </summary>
        public string AlertLevel { get; set; }

        /// <summary>
        /// 预警阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string ProcessStatus { get; set; }

        /// <summary>
        /// 处理备注
        /// </summary>
        public string ProcessRemarks { get; set; }
    }
}