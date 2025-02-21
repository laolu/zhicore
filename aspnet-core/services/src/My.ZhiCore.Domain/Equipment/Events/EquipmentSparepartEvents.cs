using System;
using My.ZhiCore.Equipment.Enums;
using Volo.Abp.Domain.Events;

namespace My.ZhiCore.Equipment.Events
{
    public class EquipmentSparepartStockChangedEvent : IEventData
    {
        public Guid SparepartId { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public StockChangeType ChangeType { get; set; }
        public int ChangeQuantity { get; set; }
    }

    public class EquipmentSparepartLowStockWarningEvent : IEventData
    {
        public Guid SparepartId { get; set; }
        public int CurrentQuantity { get; set; }
        public int MinStockWarning { get; set; }
    }
}