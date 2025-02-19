using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class PurchaseRequest : AuditableEntity, IAggregateRoot
{
    public string RequestNumber { get; private set; }
    public string Title { get; private set; }
    public DateTime RequestDate { get; private set; }
    public string RequesterId { get; private set; }
    public string RequesterDepartment { get; private set; }
    public DateTime ExpectedDeliveryDate { get; private set; }
    public PurchaseRequestStatus Status { get; private set; }
    public string Remarks { get; private set; }
    private readonly List<PurchaseRequestItem> _items = new();
    public IReadOnlyCollection<PurchaseRequestItem> Items => _items.AsReadOnly();

    private PurchaseRequest() { }

    public static PurchaseRequest Create(
        string title,
        string requesterId,
        string requesterDepartment,
        DateTime expectedDeliveryDate,
        string remarks = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("申请标题不能为空", nameof(title));

        if (string.IsNullOrWhiteSpace(requesterId))
            throw new ArgumentException("申请人ID不能为空", nameof(requesterId));

        if (string.IsNullOrWhiteSpace(requesterDepartment))
            throw new ArgumentException("申请部门不能为空", nameof(requesterDepartment));

        if (expectedDeliveryDate <= DateTime.Now)
            throw new ArgumentException("预计交付日期必须大于当前日期", nameof(expectedDeliveryDate));

        return new PurchaseRequest
        {
            RequestNumber = GenerateRequestNumber(),
            Title = title,
            RequestDate = DateTime.Now,
            RequesterId = requesterId,
            RequesterDepartment = requesterDepartment,
            ExpectedDeliveryDate = expectedDeliveryDate,
            Status = PurchaseRequestStatus.Draft,
            Remarks = remarks
        };
    }

    public void AddItem(int productId, string specification, int quantity, decimal estimatedUnitPrice)
    {
        var item = new PurchaseRequestItem(productId, specification, quantity, estimatedUnitPrice);
        _items.Add(item);
    }

    public void RemoveItem(int productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void Submit()
    {
        if (Status != PurchaseRequestStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的采购申请可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("采购申请必须包含至少一个商品");

        Status = PurchaseRequestStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != PurchaseRequestStatus.Submitted)
            throw new InvalidOperationException("只有已提交的采购申请可以审批");

        Status = PurchaseRequestStatus.Approved;
    }

    public void Reject(string reason)
    {
        if (Status != PurchaseRequestStatus.Submitted)
            throw new InvalidOperationException("只有已提交的采购申请可以驳回");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("驳回原因不能为空", nameof(reason));

        Status = PurchaseRequestStatus.Rejected;
        Remarks = reason;
    }

    public void Cancel()
    {
        if (Status != PurchaseRequestStatus.Draft && Status != PurchaseRequestStatus.Submitted)
            throw new InvalidOperationException("只有草稿或已提交状态的采购申请可以取消");

        Status = PurchaseRequestStatus.Cancelled;
    }

    private static string GenerateRequestNumber()
    {
        return $"PR{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum PurchaseRequestStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Cancelled = 4,
    Completed = 5
}