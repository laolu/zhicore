using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceAllocation : AuditableEntity
{
    public int ResourceId { get; private set; }
    public ResourceType ResourceType { get; private set; }
    public decimal Quantity { get; private set; }
    public string Notes { get; private set; }
    
    private ResourceAllocation() { }

    public static ResourceAllocation Create(
        int resourceId,
        ResourceType resourceType,
        decimal quantity,
        string notes = null)
    {
        if (resourceId <= 0)
            throw new ArgumentException("资源ID必须大于0", nameof(resourceId));

        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        return new ResourceAllocation
        {
            ResourceId = resourceId,
            ResourceType = resourceType,
            Quantity = quantity,
            Notes = notes
        };
    }

    public void UpdateQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));

        Quantity = newQuantity;
    }
}