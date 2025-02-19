using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class Equipment : AuditableEntity
{
    public string EquipmentCode { get; private set; }
    public string Name { get; private set; }
    public string Model { get; private set; }
    public string Manufacturer { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public DateTime? LastMaintenanceDate { get; private set; }
    public DateTime? NextMaintenanceDate { get; private set; }
    public EquipmentStatus Status { get; private set; }
    public string Location { get; private set; }
    public string Notes { get; private set; }

    private Equipment() { }

    public static Equipment Create(
        string equipmentCode,
        string name,
        string model,
        string manufacturer,
        DateTime purchaseDate,
        string location,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(equipmentCode))
            throw new ArgumentException("设备编码不能为空", nameof(equipmentCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("设备名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("设备型号不能为空", nameof(model));

        if (string.IsNullOrWhiteSpace(manufacturer))
            throw new ArgumentException("制造商不能为空", nameof(manufacturer));

        return new Equipment
        {
            EquipmentCode = equipmentCode,
            Name = name,
            Model = model,
            Manufacturer = manufacturer,
            PurchaseDate = purchaseDate,
            Status = EquipmentStatus.Idle,
            Location = location,
            Notes = notes
        };
    }

    public void UpdateStatus(EquipmentStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    public void ScheduleMaintenance(DateTime maintenanceDate)
    {
        if (maintenanceDate <= DateTime.Now)
            throw new ArgumentException("维护日期必须是将来的日期", nameof(maintenanceDate));

        NextMaintenanceDate = maintenanceDate;
    }

    public void CompleteMaintenance()
    {
        if (Status != EquipmentStatus.UnderMaintenance)
            throw new InvalidOperationException("只有维护中的设备可以完成维护");

        LastMaintenanceDate = DateTime.Now;
        NextMaintenanceDate = null;
        Status = EquipmentStatus.Idle;
    }

    private void ValidateStatusTransition(EquipmentStatus newStatus)
    {
        switch (Status)
        {
            case EquipmentStatus.Operating when newStatus == EquipmentStatus.UnderMaintenance:
                throw new InvalidOperationException("运行中的设备不能直接进入维护状态");
            case EquipmentStatus.Malfunction when newStatus == EquipmentStatus.Operating:
                throw new InvalidOperationException("故障设备必须先维护后才能运行");
        }
    }
}

public enum EquipmentStatus
{
    Idle = 1,              // 空闲
    Operating = 2,         // 运行中
    UnderMaintenance = 3,  // 维护中
    Malfunction = 4        // 故障
}