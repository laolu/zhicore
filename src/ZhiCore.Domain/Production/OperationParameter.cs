using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class OperationParameter : AuditableEntity
{
    public string Name { get; private set; }
    public string Value { get; private set; }
    public string Unit { get; private set; }
    public string Description { get; private set; }
    public bool IsRequired { get; private set; }
    public bool IsActive { get; private set; }
    public Operation Operation { get; private set; }
    public Guid OperationId { get; private set; }

    private OperationParameter() { }

    public static OperationParameter Create(
        string name,
        string value,
        string unit,
        string description,
        bool isRequired,
        Operation operation)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("参数名称不能为空", nameof(name));

        if (isRequired && string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("必填参数的值不能为空", nameof(value));

        return new OperationParameter
        {
            Name = name,
            Value = value,
            Unit = unit,
            Description = description,
            IsRequired = isRequired,
            IsActive = true,
            Operation = operation,
            OperationId = operation.Id
        };
    }

    public void Update(
        string value,
        string unit,
        string description)
    {
        if (IsRequired && string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("必填参数的值不能为空", nameof(value));

        Value = value;
        Unit = unit;
        Description = description;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}