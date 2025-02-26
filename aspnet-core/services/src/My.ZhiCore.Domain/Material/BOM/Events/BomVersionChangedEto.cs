using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material.BOM.Events
{
    /// <summary>
    /// BOM版本变更事件
    /// </summary>
    [EventName("My.ZhiCore.Material.BOM.VersionChanged")]
    public class BomVersionChangedEto : EtoBase
    {
        /// <summary>BOM ID</summary>
        public Guid Id { get; set; }

        /// <summary>BOM编码</summary>
        public string Code { get; set; }

        /// <summary>新版本号</summary>
        public string NewVersion { get; set; }

        /// <summary>前一版本号</summary>
        public string OldVersion { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }

        /// <summary>变更说明</summary>
        public string ChangeDescription { get; set; }

        /// <summary>前一版本ID</summary>
        public Guid? PreviousVersionId { get; set; }
    }
}