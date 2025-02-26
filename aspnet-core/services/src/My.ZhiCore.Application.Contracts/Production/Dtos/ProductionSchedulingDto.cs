using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产任务DTO
    /// </summary>
    public class ProductionTaskDto : EntityDto<Guid>
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 生产线名称
        /// </summary>
        public string ProductionLineName { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 资源调度DTO
    /// </summary>
    public class ResourceSchedulingDto
    {
        /// <summary>
        /// 生产任务ID
        /// </summary>
        public Guid ProductionTaskId { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 分配数量
        /// </summary>
        public double AllocatedQuantity { get; set; }

        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AllocationTime { get; set; }
    }

    /// <summary>
    /// 任务优先级设置DTO
    /// </summary>
    public class TaskPriorityDto
    {
        /// <summary>
        /// 生产任务ID
        /// </summary>
        public Guid ProductionTaskId { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 优先级说明
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 调度计划调整DTO
    /// </summary>
    public class ScheduleAdjustmentDto
    {
        /// <summary>
        /// 生产任务ID
        /// </summary>
        public Guid ProductionTaskId { get; set; }

        /// <summary>
        /// 新计划开始时间
        /// </summary>
        public DateTime NewStartTime { get; set; }

        /// <summary>
        /// 新计划结束时间
        /// </summary>
        public DateTime NewEndTime { get; set; }

        /// <summary>
        /// 调整原因
        /// </summary>
        public string AdjustmentReason { get; set; }
    }
}