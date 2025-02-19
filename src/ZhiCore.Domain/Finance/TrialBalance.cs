using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class TrialBalance : AuditableEntity, IAggregateRoot
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    private readonly List<TrialBalanceItem> _items = new();
    public IReadOnlyList<TrialBalanceItem> Items => _items.AsReadOnly();

    private TrialBalance() { }

    public static TrialBalance Create(int year, int month)
    {
        return new TrialBalance
        {
            Year = year,
            Month = month
        };
    }

    public void AddItem(string accountCode, string accountName, decimal beginBalance, decimal debitAmount, decimal creditAmount, decimal endBalance)
    {
        _items.Add(new TrialBalanceItem(accountCode, accountName, beginBalance, debitAmount, creditAmount, endBalance));
    }

    public bool IsBalanced()
    {
        var totalBeginBalance = _items.Sum(x => x.BeginBalance);
        var totalDebitAmount = _items.Sum(x => x.DebitAmount);
        var totalCreditAmount = _items.Sum(x => x.CreditAmount);
        var totalEndBalance = _items.Sum(x => x.EndBalance);

        return totalBeginBalance == 0 && 
               totalDebitAmount == totalCreditAmount && 
               totalEndBalance == 0;
    }

    public void Clear()
    {
        _items.Clear();
    }
}

public class TrialBalanceItem : ValueObject
{
    public string AccountCode { get; }
    public string AccountName { get; }
    public decimal BeginBalance { get; }
    public decimal DebitAmount { get; }
    public decimal CreditAmount { get; }
    public decimal EndBalance { get; }

    public TrialBalanceItem(
        string accountCode,
        string accountName,
        decimal beginBalance,
        decimal debitAmount,
        decimal creditAmount,
        decimal endBalance)
    {
        AccountCode = accountCode;
        AccountName = accountName;
        BeginBalance = beginBalance;
        DebitAmount = debitAmount;
        CreditAmount = creditAmount;
        EndBalance = endBalance;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return AccountCode;
        yield return AccountName;
        yield return BeginBalance;
        yield return DebitAmount;
        yield return CreditAmount;
        yield return EndBalance;
    }
}