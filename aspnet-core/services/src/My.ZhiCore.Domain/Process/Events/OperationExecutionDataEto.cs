using System;

namespace My.ZhiCore.Process.Events
{
    /// <summary>
    /// 工序执行数据事件
    /// </summary>
    public class OperationExecutionDataEto
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
        /// 数据类型（如：温度、压力、速度等）
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public string DataValue { get; set; }

        /// <summary>
        /// 数据单位
        /// </summary>
        public string DataUnit { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime CollectionTime { get; set; }

        /// <summary>
        /// 数据来源（如：传感器、人工录入等）
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据质量（如：正常、异常、超限等）
        /// </summary>
        public string DataQuality { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}