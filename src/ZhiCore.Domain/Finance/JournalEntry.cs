using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class JournalEntry : AuditableEntity, IAggregateRoot
{
    public string VoucherNumber { get; private set; }
    public DateTime VoucherDate { get; private set; }
    public string Description { get; private set; }
    public JournalEntryStatus Status { get; private set; }
    private readonly List<JournalEntryItem> _items = new();
    public IReadOnlyCollection<JournalEntryItem> Items => _items.AsReadOnly();

    private JournalEntry() { }

    public static JournalEntry Create(
        DateTime voucherDate,
        string description = null)
    {
        return new JournalEntry
        {
            VoucherNumber = GenerateVoucherNumber(),
            VoucherDate = voucherDate,
            Description = description,
            Status = JournalEntryStatus.Draft
        };
    }

    public void AddItem(
        string accountCode,
        decimal debit,
        decimal credit,
        string description = null)
    {
        if (Status != JournalEntryStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的凭证可以添加分录");

        var item = JournalEntryItem.Create(
            accountCode,
            debit,
            credit,
            description);

        _items.Add(item);
    }

    public void RemoveItem(string accountCode)
    {
        if (Status != JournalEntryStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的凭证可以删除分录");

        var item = _items.FirstOrDefault(i => i.AccountCode == accountCode);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void Post()
    {
        if (Status != JournalEntryStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的凭证可以过账");

        if (_items.Count == 0)
            throw new InvalidOperationException("凭证中没有分录");

        var totalDebit = _items.Sum(i => i.Debit);
        var totalCredit = _items.Sum(i => i.Credit);

        if (totalDebit != totalCredit)
            throw new InvalidOperationException("借贷不平衡");

        Status = JournalEntryStatus.Posted;
    }

    public void Cancel(string reason)
    {
        if (Status == JournalEntryStatus.Cancelled)
            throw new InvalidOperationException("凭证已经取消");

        Status = JournalEntryStatus.Cancelled;
    }

    private static string GenerateVoucherNumber()
    {
        return $"JE{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class JournalEntryItem : AuditableEntity
{
    public string AccountCode { get; private set; }
    public decimal Debit { get; private set; }
    public decimal Credit { get; private set; }
    public string Description { get; private set; }

    private JournalEntryItem() { }

    public static JournalEntryItem Create(
        string accountCode,
        decimal debit,
        decimal credit,
        string description = null)
    {
        return new JournalEntryItem
        {
            AccountCode = accountCode,
            Debit = debit,
            Credit = credit,
            Description = description
        };
    }
}

public enum JournalEntryStatus
{
    Draft = 0,
    Posted = 1,
    Cancelled = 2
}