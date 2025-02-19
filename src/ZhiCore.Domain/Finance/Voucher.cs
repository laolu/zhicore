using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class Voucher : AuditableEntity, IAggregateRoot
{
    public int Year { get; private set; }
    public int Period { get; private set; }
    public string VoucherNo { get; private set; }
    public DateTime VoucherDate { get; private set; }
    public string Summary { get; private set; }
    public VoucherStatus Status { get; private set; }
    public string Attachment { get; private set; }
    private readonly List<JournalEntry> _entries = new();
    public IReadOnlyCollection<JournalEntry> Entries => _entries.AsReadOnly();

    private Voucher() { }

    public static Voucher Create(
        int year,
        int period,
        string voucherNo,
        DateTime voucherDate,
        string summary,
        string attachment = null)
    {
        return new Voucher
        {
            Year = year,
            Period = period,
            VoucherNo = voucherNo,
            VoucherDate = voucherDate,
            Summary = summary,
            Status = VoucherStatus.Draft,
            Attachment = attachment
        };
    }

    public void AddEntry(string accountCode, decimal debit, decimal credit, string memo = null)
    {
        if (Status != VoucherStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的凭证才能修改");

        var entry = JournalEntry.Create(accountCode, debit, credit, memo);
        _entries.Add(entry);
    }

    public void Submit()
    {
        if (Status != VoucherStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的凭证才能提交");

        ValidateEntries();
        Status = VoucherStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != VoucherStatus.Submitted)
            throw new InvalidOperationException("只有已提交的凭证才能审核");

        Status = VoucherStatus.Approved;
    }

    public void Reject()
    {
        if (Status != VoucherStatus.Submitted)
            throw new InvalidOperationException("只有已提交的凭证才能驳回");

        Status = VoucherStatus.Draft;
    }

    private void ValidateEntries()
    {
        if (_entries.Count == 0)
            throw new InvalidOperationException("凭证必须包含会计分录");

        decimal totalDebit = 0;
        decimal totalCredit = 0;

        foreach (var entry in _entries)
        {
            totalDebit += entry.Debit;
            totalCredit += entry.Credit;
        }

        if (totalDebit != totalCredit)
            throw new InvalidOperationException("借贷方金额不平衡");
    }
}

public enum VoucherStatus
{
    Draft = 1,      // 草稿
    Submitted = 2,   // 已提交
    Approved = 3,    // 已审核
    Voided = 4       // 已作废
}