using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionResource : AuditableEntity
{
    public string ResourceCode { get; private set; }
    public string Name { get; private set; }
    public ResourceType Type { get; private set; }
    public ResourceStatus Status { get; private set; }
    public string Specification { get; private set; }
    public string Location { get; private set; }
    public string Department { get; private set; }
    public DateTime? LastMaintenanceDate { get; private set; }
    public DateTime? NextMaintenanceDate { get; private set; }
    public string Notes { get; private set; }
    
    private ProductionResource() { }

    public static ProductionResource Create(
        string resourceCode,
        string name,
        ResourceType type,
        string specification = null,
        string location = null,
        string department = null,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(resourceCode))
            throw new ArgumentException("资源编码不能为空", nameof(resourceCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("资源名称不能为空", nameof(name));

        return new ProductionResource
        {
            ResourceCode = resourceCode,
            Name = name,
            Type = type,
            Status = ResourceStatus.Available,
            Specification = specification,
            Location = location,
            Department = department,
            Notes = notes
        };
    }

    public void UpdateStatus(ResourceStatus newStatus)
    {
        if (newStatus == Status)
            return;

        Status = newStatus;
    }

    public void RecordMaintenance(DateTime maintenanceDate, DateTime nextMaintenanceDate)
    {
        if (nextMaintenanceDate <= maintenanceDate)
            throw new ArgumentException("下次保养日期必须晚于本次保养日期");

        LastMaintenanceDate = maintenanceDate;
        NextMaintenanceDate = nextMaintenanceDate;
    }

    public void UpdateLocation(string newLocation)
    {
        Location = newLocation;
    }

    public void UpdateDepartment(string newDepartment)
    {
        Department = newDepartment;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}

public enum ResourceType
{
    Equipment = 1,  // 设备
    Tool = 2,       // 工装
    Personnel = 3   // 人员
}

public enum ResourceStatus
{
    Available = 1,    // 可用
    InUse = 2,        // 使用中
    Maintenance = 3,   // 维护中
    Malfunction = 4,   // 故障
    Retired = 5       // 报废
}