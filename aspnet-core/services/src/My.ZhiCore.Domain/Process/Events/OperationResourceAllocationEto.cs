using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源分配事件
    /// </summary>
    public class OperationResourceAllocationEto
    {
        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 分配类型（分配/取消分配）
        /// </summary>
        public string AllocationType { get; set; }

        /// <summary>
        /// 分配数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlannedEndTime { get; set; }

        /// <summary>
        /// 分配时间
        /// </summary>
        public DateTime AllocationTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 分配原因
        /// </summary>
        public string AllocationReason { get; set; }
    }
}