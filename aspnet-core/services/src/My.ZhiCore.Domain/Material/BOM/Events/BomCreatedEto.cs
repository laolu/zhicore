using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material.BOM.Events
{
    /// <summary>
    /// BOM创建事件
    /// </summary>
    [EventName("My.ZhiCore.Material.BOM.Created")]
    public class BomCreatedEto : EtoBase
    {
        /// <summary>BOM ID</summary>
        public Guid Id { get; set; }

        /// <summary>BOM编码</summary>
        public string Code { get; set; }

        /// <summary>BOM名称</summary>
        public string Name { get; set; }

        /// <summary>BOM版本</summary>
        public string Version { get; set; }

        /// <summary>BOM类型</summary>
        public BomType Type { get; set; }

        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; set; }
    }
}