using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源预约事件
    /// </summary>
    public class OperationResourceReservationEto
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
        /// 预约类型（预约/取消预约）
        /// </summary>
        public string ReservationType { get; set; }

        /// <summary>
        /// 预约数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime ReservationStartTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime ReservationEndTime { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReservationTime { get; set; }

        /// <summary>
        /// 预约人ID
        /// </summary>
        public Guid? OperatorId { get; set; }

        /// <summary>
        /// 预约原因
        /// </summary>
        public string ReservationReason { get; set; }

        /// <summary>
        /// 预约状态（待确认/已确认/已拒绝）
        /// </summary>
        public string ReservationStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}