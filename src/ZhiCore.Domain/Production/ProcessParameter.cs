using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProcessParameter : AuditableEntity
{
    public int ProcessId { get; private set; }
    public string ParameterCode { get; private set; }
    public string Description { get; private set; }
    public string Version { get; private set; }
    public ProcessParameterStatus Status { get; private set; }
    public string Notes { get; private set; }
    public string GroupCode { get; private set; }        // 分组编码
    public string GroupName { get; private set; }        // 分组名称
    public string Unit { get; private set; }            // 单位
    public ParameterType ParameterType { get; private set; }    // 参数类型
    public bool IsRequired { get; private set; }        // 是否必填
    public string DefaultValue { get; private set; }    // 默认值
    public decimal? MinValue { get; private set; }      // 最小值
    public decimal? MaxValue { get; private set; }      // 最大值
    public string ValidationRules { get; private set; } // 验证规则
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    private ProcessParameter() { }

    public static ProcessParameter Create(
        int processId,
        string parameterCode,
        string description,
        string version,
        DateTime effectiveDate,
        string unit,
        ParameterType parameterType,
        bool isRequired,
        string groupCode = null,
        string groupName = null,
        string defaultValue = null,
        decimal? minValue = null,
        decimal? maxValue = null,
        string validationRules = null,
        DateTime? expirationDate = null,
        string notes = null)
    {
        if (processId <= 0)
            throw new ArgumentException("工序ID必须大于0", nameof(processId));

        if (string.IsNullOrWhiteSpace(parameterCode))
            throw new ArgumentException("参数编码不能为空", nameof(parameterCode));

        if (string.IsNullOrWhiteSpace(version))
            throw new ArgumentException("版本号不能为空", nameof(version));

        return new ProcessParameter
        {
            ProcessId = processId,
            ParameterCode = parameterCode,
            Description = description,
            Version = version,
            Status = ProcessParameterStatus.Draft,
            EffectiveDate = effectiveDate,
            ExpirationDate = expirationDate,
            Notes = notes,
            Unit = unit,
            ParameterType = parameterType,
            IsRequired = isRequired,
            DefaultValue = defaultValue,
            MinValue = minValue,
            MaxValue = maxValue,
            ValidationRules = validationRules,
            GroupCode = groupCode,
            GroupName = groupName
        };
    }

    public void UpdateStatus(ProcessParameterStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    private void ValidateStatusTransition(ProcessParameterStatus newStatus)
    {
        switch (Status)
        {
            case ProcessParameterStatus.Draft when newStatus != ProcessParameterStatus.Active:
                throw new InvalidOperationException("草稿状态只能转换为生效状态");
            case ProcessParameterStatus.Active when newStatus != ProcessParameterStatus.Inactive:
                throw new InvalidOperationException("生效状态只能转换为失效状态");
            case ProcessParameterStatus.Inactive:
                throw new InvalidOperationException("失效状态不能转换为其他状态");
        }
    }
}

public enum ProcessParameterStatus
{
    Draft = 1,    // 草稿
    Active = 2,   // 生效
    Inactive = 3  // 失效
}

public enum ParameterType
{
    Numeric = 1,      // 数值型
    Text = 2,         // 文本型
    Boolean = 3,      // 布尔型
    DateTime = 4,     // 日期时间型
    Enum = 5          // 枚举型
}