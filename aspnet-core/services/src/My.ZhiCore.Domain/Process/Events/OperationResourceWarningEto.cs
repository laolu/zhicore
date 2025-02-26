using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序资源预警事件
    /// </summary>
    public class OperationResourceWarningEto
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
        /// 预警类型（库存不足/即将耗尽/超出阈值等）
        /// </summary>
        public string WarningType { get; set; }

        /// <summary>
        /// 当前数量
        /// </summary>
        public decimal CurrentQuantity { get; set; }

        /// <summary>
        /// 阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 预警等级（低/中/高）
        /// </summary>
        public string WarningLevel { get; set; }

        /// <summary>
        /// 预警时间
        /// </summary>
        public DateTime WarningTime { get; set; }

        /// <summary>
        /// 预警描述
        /// </summary>
        public string WarningDescription { get; set; }

        /// <summary>
        /// 建议措施
        /// </summary>
        public string SuggestedAction { get; set; }
    }
}