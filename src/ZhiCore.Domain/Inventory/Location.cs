using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class Location : Entity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public LocationType Type { get; private set; }
    public decimal MaxCapacity { get; private set; }
    public decimal CurrentCapacity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? LastModifiedTime { get; private set; }

    public Guid WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }

    private Location() { }

    public static Location Create(
        string code,
        string name,
        LocationType type,
        decimal maxCapacity,
        Warehouse warehouse)
    {
        var location = new Location
        {
            Code = code,
            Name = name,
            Type = type,
            MaxCapacity = maxCapacity,
            CurrentCapacity = 0,
            IsActive = true,
            CreatedTime = DateTime.Now,
            WarehouseId = warehouse.Id,
            Warehouse = warehouse
        };

        warehouse.AddLocation(location);
        return location;
    }

    public void Update(
        string name,
        LocationType type,
        decimal maxCapacity)
    {
        Name = name;
        Type = type;
        MaxCapacity = maxCapacity;
        LastModifiedTime = DateTime.Now;
    }

    public void UpdateCapacity(decimal capacity)
    {
        if (capacity > MaxCapacity)
        {
            throw new DomainException("当前容量不能超过最大容量");
        }

        CurrentCapacity = capacity;
        LastModifiedTime = DateTime.Now;
    }

    public void Activate()
    {
        IsActive = true;
        LastModifiedTime = DateTime.Now;
    }

    public void Deactivate()
    {
        IsActive = false;
        LastModifiedTime = DateTime.Now;
    }
}