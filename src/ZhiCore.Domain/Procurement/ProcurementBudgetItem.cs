using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class ProcurementBudgetItem : Entity
{
    public string DepartmentCode { get; private set; }
    public string DepartmentName { get; private set; }
    public string CategoryCode { get; private set; }
    public string CategoryName { get; private set; }
    public decimal Amount { get; private set; }
    public decimal UsedAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public string Remark { get; private set; }

    private ProcurementBudgetItem() { }

    public ProcurementBudgetItem(
        string departmentCode,
        string departmentName,
        string categoryCode,
        string categoryName,
        decimal amount,
        string remark = null)
    {
        if (string.IsNullOrWhiteSpace(departmentCode))
            throw new ArgumentException("部门编码不能为空", nameof(departmentCode));

        if (string.IsNullOrWhiteSpace(departmentName))
            throw new ArgumentException("部门名称不能为空", nameof(departmentName));

        if (string.IsNullOrWhiteSpace(categoryCode))
            throw new ArgumentException("类别编码不能为空", nameof(categoryCode));

        if (string.IsNullOrWhiteSpace(categoryName))
            throw new ArgumentException("类别名称不能为空", nameof(categoryName));

        if (amount <= 0)
            throw new ArgumentException("预算金额必须大于0", nameof(amount));

        DepartmentCode = departmentCode;
        DepartmentName = departmentName;
        CategoryCode = categoryCode;
        CategoryName = categoryName;
        Amount = amount;
        Remark = remark;
        UsedAmount = 0;
        RemainingAmount = amount;
    }

    public void UpdateUsedAmount(decimal amount)
    {
        if (amount < 0)
            throw new InvalidOperationException("使用金额不能为负数");

        if (amount > RemainingAmount)
            throw new InvalidOperationException("使用金额不能超过剩余预算");

        UsedAmount += amount;
        RemainingAmount = Amount - UsedAmount;
    }
}