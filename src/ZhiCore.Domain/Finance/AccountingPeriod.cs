using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class AccountingPeriod : AuditableEntity, IAggregateRoot
{
    public int Year { get; private set; }
    public int Period { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsClosed { get; private set; }
    public string Description { get; private set; }

    private AccountingPeriod() { }

    public static AccountingPeriod Create(
        int year,
        int period,
        DateTime startDate,
        DateTime endDate,
        string description = null)
    {
        if (period < 1 || period > 12)
            throw new ArgumentException("会计期间必须在1-12之间");

        if (startDate >= endDate)
            throw new ArgumentException("开始日期必须早于结束日期");

        return new AccountingPeriod
        {
            Year = year,
            Period = period,
            StartDate = startDate,
            EndDate = endDate,
            Description = description,
            IsClosed = false
        };
    }

    public void Close()
    {
        if (IsClosed)
            throw new InvalidOperationException("会计期间已关闭");

        IsClosed = true;
    }

    public void Reopen()
    {
        if (!IsClosed)
            throw new InvalidOperationException("会计期间未关闭");

        IsClosed = false;
    }
}