using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class Account : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public AccountCategory Category { get; private set; }
    public string ParentCode { get; private set; }
    public bool IsActive { get; private set; }
    public string Description { get; private set; }
    public decimal Balance { get; private set; }
    public BalanceDirection BalanceDirection { get; private set; }
    public SettlementType SettlementType { get; private set; }
    public int Level { get; private set; }

    private Account() { }

    public static Account Create(
        string code,
        string name,
        AccountType type,
        AccountCategory category,
        BalanceDirection balanceDirection,
        SettlementType settlementType,
        string parentCode = null,
        string description = null)
    {
        var account = new Account
        {
            Code = code,
            Name = name,
            Type = type,
            Category = category,
            BalanceDirection = balanceDirection,
            SettlementType = settlementType,
            ParentCode = parentCode,
            Description = description,
            IsActive = true,
            Balance = 0
        };

        account.CalculateLevel();
        return account;
    }

    private void CalculateLevel()
    {
        Level = string.IsNullOrEmpty(ParentCode) ? 1 : (ParentCode.Length / 4) + 1;
    }

    public void UpdateBalance(decimal amount)
    {
        var newBalance = Balance + amount;
        ValidateBalance(newBalance);
        Balance = newBalance;
    }

    private void ValidateBalance(decimal newBalance)
    {
        if (BalanceDirection == BalanceDirection.Debit && newBalance < 0)
            throw new InvalidOperationException("借方科目余额不能为负");

        if (BalanceDirection == BalanceDirection.Credit && newBalance > 0)
            throw new InvalidOperationException("贷方科目余额不能为正");
    }

    public void SettleAccount()
    {
        if (SettlementType == SettlementType.None)
            return;

        if (SettlementType == SettlementType.ToProfit && Type != AccountType.Revenue && Type != AccountType.Expense)
            throw new InvalidOperationException("只有收入和费用类科目可以结转损益");

        Balance = 0;
    }

    public void Deactivate()
    {
        if (Balance != 0)
            throw new InvalidOperationException("账户余额不为零，无法停用");

        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}

public enum AccountType
{
    Asset = 1,      // 资产类
    Liability = 2,  // 负债类
    Equity = 3,     // 权益类
    Revenue = 4,    // 收入类
    Expense = 5     // 费用类
}

public enum AccountCategory
{
    Current = 1,    // 流动类
    NonCurrent = 2, // 非流动类
    Operating = 3,  // 经营类
    NonOperating = 4 // 非经营类
}

public enum BalanceDirection
{
    Debit = 1,    // 借方
    Credit = 2     // 贷方
}

public enum SettlementType
{
    None = 0,      // 不需结转
    ToProfit = 1,   // 结转损益
    ToNext = 2      // 结转下期
}