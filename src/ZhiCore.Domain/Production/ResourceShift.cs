using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceShift : AuditableEntity
{
    public int ResourceId { get; private set; }
    public ResourceType ResourceType { get; private set; }
    public int ShiftDefinitionId { get; private set; }
    public DateTime ShiftDate { get; private set; }
    public bool IsConfirmed { get; private set; }
    public string Notes { get; private set; }
    
    private ResourceShift() { }
    
    public static ResourceShift Create(
        int resourceId,
        ResourceType resourceType,
        int shiftDefinitionId,
        DateTime shiftDate,
        bool isConfirmed = false,
        string notes = null)
    {
        if (resourceId <= 0)
            throw new ArgumentException("资源ID必须大于0", nameof(resourceId));
            
        if (shiftDefinitionId <= 0)
            throw new ArgumentException("班次定义ID必须大于0", nameof(shiftDefinitionId));
            
        return new ResourceShift
        {
            ResourceId = resourceId,
            ResourceType = resourceType,
            ShiftDefinitionId = shiftDefinitionId,
            ShiftDate = shiftDate.Date,
            IsConfirmed = isConfirmed,
            Notes = notes
        };
    }
    
    public void UpdateShiftDefinition(int shiftDefinitionId)
    {
        if (shiftDefinitionId <= 0)
            throw new ArgumentException("班次定义ID必须大于0", nameof(shiftDefinitionId));
            
        ShiftDefinitionId = shiftDefinitionId;
    }
    
    public void UpdateShiftDate(DateTime shiftDate)
    {
        ShiftDate = shiftDate.Date;
    }
    
    public void SetConfirmation(bool isConfirmed)
    {
        IsConfirmed = isConfirmed;
    }
    
    public void UpdateNotes(string notes)
    {
        Notes = notes;
    }
}