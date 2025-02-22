using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产计划DTO
    /// </summary>
    public class ProductionPlanDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string PlanNumber { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        public ProductionPlanStatus Status { get; set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public int PlannedQuantity { get; set; }

        /// <summary>
        /// 实际产量
        /// </summary>
        public int ActualQuantity { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建生产计划DTO
    /// </summary>
    public class CreateProductionPlanDto
    {
        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public int PlannedQuantity { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新生产计划DTO
    /// </summary>
    public class UpdateProductionPlanDto
    {
        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public int PlannedQuantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 生产计划状态枚举
    /// </summary>
    public enum ProductionPlanStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,

        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 1,

        /// <summary>
        /// 执行中
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 4
    }
}