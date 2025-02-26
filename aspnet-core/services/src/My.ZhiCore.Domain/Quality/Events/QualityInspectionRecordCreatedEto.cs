using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Quality.Events
{
    /// <summary>
    /// 质量检验记录创建事件
    /// </summary>
    [EventName("My.ZhiCore.Quality.QualityInspectionRecordCreated")]
    public class QualityInspectionRecordCreatedEto
    {
        /// <summary>检验记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>检验项目ID</summary>
        public Guid InspectionItemId { get; set; }

        /// <summary>检验类型</summary>
        public string InspectionType { get; set; }

        /// <summary>检验结果</summary>
        public string Result { get; set; }

        /// <summary>是否合格</summary>
        public bool IsQualified { get; set; }

        /// <summary>检验数据</summary>
        public Dictionary<string, string> InspectionData { get; set; }

        /// <summary>检验时间</summary>
        public DateTime InspectionTime { get; set; }

        /// <summary>检验人ID</summary>
        public Guid InspectorId { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }
}