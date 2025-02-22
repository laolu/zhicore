using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序质量检验结果事件
    /// </summary>
    public class OperationQualityResultEto
    {
        /// <summary>
        /// 工序执行ID
        /// </summary>
        public Guid ExecutionId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 检验项目ID
        /// </summary>
        public Guid InspectionItemId { get; set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        public string InspectionStandard { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; set; }

        /// <summary>
        /// 检验结果值
        /// </summary>
        public string ResultValue { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 不合格原因
        /// </summary>
        public string UnqualifiedReason { get; set; }

        /// <summary>
        /// 检验人ID
        /// </summary>
        public Guid InspectorId { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime InspectionTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}