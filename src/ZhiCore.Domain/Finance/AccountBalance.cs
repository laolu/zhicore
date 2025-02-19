using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class AccountBalance : AuditableEntity, IAggregateRoot
{
    public string AccountCode { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }
    public decimal BeginBalance { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal EndBalance { get; private set; }

    private AccountBalance() { }

    public static AccountBalance Create(
        string accountCode,
        int year,
        int month,
        decimal beginBalance = 0)
    {
        return new AccountBalance
        {
            AccountCode = accountCode,
            Year = year,
            Month = month,
            BeginBalance = beginBalance,
            DebitAmount = 0,
            CreditAmount = 0,
            EndBalance = beginBalance
        };
    }

    public void RecordTransaction(decimal debit, decimal credit)
    {
        DebitAmount += debit;
        CreditAmount += credit;
        EndBalance = BeginBalance + DebitAmount - CreditAmount;
    }

    public void CarryForward()
    {
        BeginBalance = EndBalance;
        DebitAmount = 0;
        CreditAmount = 0;
        EndBalance = BeginBalance;
    }

    public void Reset()
    {
        BeginBalance = 0;
        DebitAmount = 0;
        CreditAmount = 0;
        EndBalance = 0;
    }
}