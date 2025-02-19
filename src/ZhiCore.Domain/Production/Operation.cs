using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class Operation : AuditableEntity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal StandardTime { get; private set; }  // 标准工时（分钟）
    public decimal StandardCost { get; private set; }  // 标准成本
    public string WorkCenter { get; private set; }     // 工作中心
    public bool IsActive { get; private set; }
    public string Notes { get; private set; }
    private readonly List<OperationParameter> _parameters = new();
    public IReadOnlyCollection<OperationParameter> Parameters => _parameters.AsReadOnly();

    private Operation() { }

    public static Operation Create(
        string code,
        string name,
        string description,
        decimal standardTime,
        decimal standardCost,
        string workCenter,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("工序编码不能为空", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("工序名称不能为空", nameof(name));

        if (standardTime <= 0)
            throw new ArgumentException("标准工时必须大于0", nameof(standardTime));

        if (standardCost < 0)
            throw new ArgumentException("标准成本不能为负数", nameof(standardCost));

        if (string.IsNullOrWhiteSpace(workCenter))
            throw new ArgumentException("工作中心不能为空", nameof(workCenter));

        return new Operation
        {
            Code = code,
            Name = name,
            Description = description,
            StandardTime = standardTime,
            StandardCost = standardCost,
            WorkCenter = workCenter,
            IsActive = true,
            Notes = notes
        };
    }

    public void Update(
        string name,
        string description,
        decimal standardTime,
        decimal standardCost,
        string workCenter,
        string notes)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("工序名称不能为空", nameof(name));

        if (standardTime <= 0)
            throw new ArgumentException("标准工时必须大于0", nameof(standardTime));

        if (standardCost < 0)
            throw new ArgumentException("标准成本不能为负数", nameof(standardCost));

        if (string.IsNullOrWhiteSpace(workCenter))
            throw new ArgumentException("工作中心不能为空", nameof(workCenter));

        Name = name;
        Description = description;
        StandardTime = standardTime;
        StandardCost = standardCost;
        WorkCenter = workCenter;
        Notes = notes;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public OperationParameter AddParameter(
        string name,
        string value,
        string unit,
        string description,
        bool isRequired)
    {
        var parameter = OperationParameter.Create(
            name,
            value,
            unit,
            description,
            isRequired,
            this);

        _parameters.Add(parameter);
        return parameter;
    }

    public void RemoveParameter(OperationParameter parameter)
    {
        if (parameter.OperationId != Id)
            throw new ArgumentException("参数不属于当前工序", nameof(parameter));

        _parameters.Remove(parameter);
    }
}