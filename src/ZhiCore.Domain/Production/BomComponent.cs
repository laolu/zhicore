using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class BomComponent : AuditableEntity
{
    public int ComponentId { get; private set; }
    public decimal Quantity { get; private set; }
    public string Position { get; private set; }
    public string Notes { get; private set; }
    public int BillOfMaterialId { get; private set; }
    public BillOfMaterial BillOfMaterial { get; private set; }

    private BomComponent() { }

    public static BomComponent Create(
        int componentId,
        decimal quantity,
        string position = null,
        string notes = null)
    {
        if (componentId <= 0)
            throw new ArgumentException("组件ID必须大于0", nameof(componentId));

        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        return new BomComponent
        {
            ComponentId = componentId,
            Quantity = quantity,
            Position = position,
            Notes = notes
        };
    }

    public void UpdateQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));

        Quantity = newQuantity;
    }

    public void UpdatePosition(string newPosition)
    {
        Position = newPosition;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}