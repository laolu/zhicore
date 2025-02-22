using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序质量检验事件
    /// </summary>
    public class OperationQualityInspectionEto
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
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 检验数值
        /// </summary>
        public decimal? MeasuredValue { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public decimal? StandardValue { get; set; }

        /// <summary>
        /// 上限值
        /// </summary>
        public decimal? UpperLimit { get; set; }

        /// <summary>
        /// 下限值
        /// </summary>
        public decimal? LowerLimit { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime InspectionTime { get; set; }

        /// <summary>
        /// 检验人ID
        /// </summary>
        public Guid InspectorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}