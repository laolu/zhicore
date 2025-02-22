using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material.BOM.Events
{
    /// <summary>
    /// BOM结构变更事件（用于子装配变更）
    /// </summary>
    [EventName("My.ZhiCore.Material.BOM.StructureChanged")]
    public class BomStructureChangedEto : EtoBase
    {
        /// <summary>BOM项ID</summary>
        public Guid BomItemId { get; set; }

        /// <summary>所属物料清单ID</summary>
        public Guid BillOfMaterialId { get; set; }

        /// <summary>子装配BOM ID</summary>
        public Guid SubBomId { get; set; }

        /// <summary>是否为新增子装配</summary>
        public bool IsNewSubAssembly { get; set; }
    }
}