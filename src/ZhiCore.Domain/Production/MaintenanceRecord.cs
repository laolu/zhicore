using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class MaintenanceRecord : AuditableEntity
{
    public int EquipmentId { get; private set; }
    public DateTime MaintenanceDate { get; private set; }
    public MaintenanceType Type { get; private set; }
    public string Description { get; private set; }
    public string Technician { get; private set; }
    public decimal Cost { get; private set; }
    public string Result { get; private set; }
    public string Notes { get; private set; }

    private MaintenanceRecord() { }

    public static MaintenanceRecord Create(
        int equipmentId,
        DateTime maintenanceDate,
        MaintenanceType type,
        string description,
        string technician,
        decimal cost,
        string result,
        string notes = null)
    {
        if (equipmentId <= 0)
            throw new ArgumentException("设备ID必须大于0", nameof(equipmentId));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("维护内容不能为空", nameof(description));

        if (string.IsNullOrWhiteSpace(technician))
            throw new ArgumentException("维护人员不能为空", nameof(technician));

        if (cost < 0)
            throw new ArgumentException("维护成本不能为负数", nameof(cost));

        if (string.IsNullOrWhiteSpace(result))
            throw new ArgumentException("维护结果不能为空", nameof(result));

        return new MaintenanceRecord
        {
            EquipmentId = equipmentId,
            MaintenanceDate = maintenanceDate,
            Type = type,
            Description = description,
            Technician = technician,
            Cost = cost,
            Result = result,
            Notes = notes
        };
    }

    public void UpdateResult(string newResult)
    {
        if (string.IsNullOrWhiteSpace(newResult))
            throw new ArgumentException("维护结果不能为空", nameof(newResult));

        Result = newResult;
    }

    public void UpdateCost(decimal newCost)
    {
        if (newCost < 0)
            throw new ArgumentException("维护成本不能为负数", nameof(newCost));

        Cost = newCost;
    }
}

public enum MaintenanceType
{
    Routine = 1,     // 日常保养
    Preventive = 2,  // 预防性维护
    Repair = 3,      // 故障维修
    Overhaul = 4     // 大修
}