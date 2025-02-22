using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产质量DTO
    /// </summary>
    public class ProductionQualityDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime InspectionTime { get; set; }

        /// <summary>
        /// 检测批次
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 检测数量
        /// </summary>
        public int InspectionQuantity { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int UnqualifiedQuantity { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal QualificationRate { get; set; }

        /// <summary>
        /// 质量检测项目
        /// </summary>
        public List<QualityInspectionItemDto> InspectionItems { get; set; }

        /// <summary>
        /// 质量问题描述
        /// </summary>
        public string QualityIssueDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string HandlingMeasures { get; set; }

        /// <summary>
        /// 检测人员ID
        /// </summary>
        public Guid InspectorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 质量检测项目DTO
    /// </summary>
    public class QualityInspectionItemDto
    {
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 实测值
        /// </summary>
        public string ActualValue { get; set; }

        /// <summary>
        /// 允许误差范围
        /// </summary>
        public string Tolerance { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 不合格原因
        /// </summary>
        public string UnqualifiedReason { get; set; }
    }

    /// <summary>
    /// 创建生产质量DTO
    /// </summary>
    public class CreateProductionQualityDto
    {
        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 检测批次
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 检测数量
        /// </summary>
        public int InspectionQuantity { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 质量检测项目
        /// </summary>
        public List<QualityInspectionItemDto> InspectionItems { get; set; }

        /// <summary>
        /// 质量问题描述
        /// </summary>
        public string QualityIssueDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string HandlingMeasures { get; set; }

        /// <summary>
        /// 检测人员ID
        /// </summary>
        public Guid InspectorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新生产质量DTO
    /// </summary>
    public class UpdateProductionQualityDto
    {
        /// <summary>
        /// 检测数量
        /// </summary>
        public int InspectionQuantity { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 质量检测项目
        /// </summary>
        public List<QualityInspectionItemDto> InspectionItems { get; set; }

        /// <summary>
        /// 质量问题描述
        /// </summary>
        public string QualityIssueDescription { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string HandlingMeasures { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}