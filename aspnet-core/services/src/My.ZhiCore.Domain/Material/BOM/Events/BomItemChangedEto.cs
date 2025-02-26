using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material.BOM.Events
{
    /// <summary>
    /// BOM项变更事件
    /// </summary>
    [EventName("My.ZhiCore.Material.BOM.ItemChanged")]
    public class BomItemChangedEto : EtoBase
    {
        /// <summary>BOM项ID</summary>
        public Guid Id { get; set; }

        /// <summary>所属物料清单ID</summary>
        public Guid BillOfMaterialId { get; set; }

        /// <summary>物料ID</summary>
        public Guid MaterialId { get; set; }

        /// <summary>变更类型</summary>
        public BomItemChangeType ChangeType { get; set; }

        /// <summary>变更前数量</summary>
        public decimal? OldQuantity { get; set; }

        /// <summary>变更后数量</summary>
        public decimal? NewQuantity { get; set; }

        /// <summary>变更前位置</summary>
        public string OldPosition { get; set; }

        /// <summary>变更后位置</summary>
        public string NewPosition { get; set; }
    }

    /// <summary>
    /// BOM项变更类型
    /// </summary>
    public enum BomItemChangeType
    {
        /// <summary>数量变更</summary>
        QuantityChanged = 1,

        /// <summary>位置变更</summary>
        PositionChanged = 2,

        /// <summary>状态变更（启用/禁用）</summary>
        StatusChanged = 3
    }
}