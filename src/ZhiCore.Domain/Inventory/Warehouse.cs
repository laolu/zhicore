using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class Warehouse : AggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string ContactPerson { get; private set; }
    public string ContactPhone { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public DateTime? LastModifiedTime { get; private set; }
    
    private readonly List<Location> _locations = new();
    public IReadOnlyCollection<Location> Locations => _locations.AsReadOnly();

    private Warehouse() { }

    public static Warehouse Create(
        string code,
        string name,
        string address,
        string contactPerson,
        string contactPhone)
    {
        var warehouse = new Warehouse
        {
            Code = code,
            Name = name,
            Address = address,
            ContactPerson = contactPerson,
            ContactPhone = contactPhone,
            IsActive = true,
            CreatedTime = DateTime.Now
        };

        return warehouse;
    }

    public void Update(
        string name,
        string address,
        string contactPerson,
        string contactPhone)
    {
        Name = name;
        Address = address;
        ContactPerson = contactPerson;
        ContactPhone = contactPhone;
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

    public void AddLocation(Location location)
    {
        _locations.Add(location);
    }

    public void RemoveLocation(Location location)
    {
        _locations.Remove(location);
    }
}