using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessRoute : AuditableEntity, IAggregateRoot
{
    public string RouteCode { get; private set; }
    public string ProductCode { get; private set; }
    public string RouteName { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    private readonly List<ProcessOperation> _operations = new();
    public IReadOnlyCollection<ProcessOperation> Operations => _operations.AsReadOnly();

    private ProcessRoute() { }

    public static ProcessRoute Create(
        string productCode,
        string routeName,
        string description = null)
    {
        var route = new ProcessRoute
        {
            RouteCode = GenerateRouteCode(),
            ProductCode = productCode,
            RouteName = routeName,
            Description = description,
            IsActive = true
        };

        return route;
    }

    public void AddOperation(
        string operationCode,
        string operationName,
        int sequence,
        string workstationCode,
        decimal standardTime,
        string description = null)
    {
        if (_operations.Exists(o => o.Sequence == sequence))
            throw new InvalidOperationException("工序序号已存在");

        var operation = ProcessOperation.Create(
            operationCode,
            operationName,
            sequence,
            workstationCode,
            standardTime,
            description);

        _operations.Add(operation);
    }

    public void RemoveOperation(string operationCode)
    {
        var operation = _operations.Find(o => o.OperationCode == operationCode);
        if (operation != null)
        {
            _operations.Remove(operation);
        }
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static string GenerateRouteCode()
    {
        return $"PR{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class ProcessOperation : AuditableEntity
{
    public string OperationCode { get; private set; }
    public string OperationName { get; private set; }
    public int Sequence { get; private set; }
    public string WorkstationCode { get; private set; }
    public decimal StandardTime { get; private set; }
    public string Description { get; private set; }

    private ProcessOperation() { }

    public static ProcessOperation Create(
        string operationCode,
        string operationName,
        int sequence,
        string workstationCode,
        decimal standardTime,
        string description = null)
    {
        return new ProcessOperation
        {
            OperationCode = operationCode,
            OperationName = operationName,
            Sequence = sequence,
            WorkstationCode = workstationCode,
            StandardTime = standardTime,
            Description = description
        };
    }
}