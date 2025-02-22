using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源变更事件
    /// </summary>
    public class OperationResourceChangedEto
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
        /// 变更类型（新增/移除/更新）
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 原数量
        /// </summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 新数量
        /// </summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 变更人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }
    }
}