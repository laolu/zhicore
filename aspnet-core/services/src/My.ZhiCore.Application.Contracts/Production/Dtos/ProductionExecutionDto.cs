using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产执行DTO
    /// </summary>
    public class ProductionExecutionDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 执行编号
        /// </summary>
        public string ExecutionNumber { get; set; }

        /// <summary>
        /// 生产计划ID
        /// </summary>
        public Guid ProductionPlanId { get; set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ProductionExecutionStatus Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public int PlannedQuantity { get; set; }

        /// <summary>
        /// 完成产量
        /// </summary>
        public int CompletedQuantity { get; set; }

        /// <summary>
        /// 不良品数量
        /// </summary>
        public int DefectiveQuantity { get; set; }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建生产执行DTO
    /// </summary>
    public class CreateProductionExecutionDto
    {
        /// <summary>
        /// 生产计划ID
        /// </summary>
        public Guid ProductionPlanId { get; set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 计划产量
        /// </summary>
        public int PlannedQuantity { get; set; }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新生产执行DTO
    /// </summary>
    public class UpdateProductionExecutionDto
    {
        /// <summary>
        /// 完成产量
        /// </summary>
        public int CompletedQuantity { get; set; }

        /// <summary>
        /// 不良品数量
        /// </summary>
        public int DefectiveQuantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 生产执行状态枚举
    /// </summary>
    public enum ProductionExecutionStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// 执行中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 暂停
        /// </summary>
        Paused = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 3,

        /// <summary>
        /// 已终止
        /// </summary>
        Terminated = 4
    }
}