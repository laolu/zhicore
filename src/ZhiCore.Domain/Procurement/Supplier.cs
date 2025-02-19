using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class Supplier : AuditableEntity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string ContactPerson { get; private set; }
    public string ContactPhone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public SupplierStatus Status { get; private set; }
    public string TaxNumber { get; private set; }
    public string BankAccount { get; private set; }
    public string BankName { get; private set; }

    private Supplier() { }

    public static Supplier Create(
        string code,
        string name,
        string contactPerson,
        string contactPhone,
        string email,
        string address,
        string taxNumber = null,
        string bankAccount = null,
        string bankName = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("供应商编码不能为空", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("供应商名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(contactPerson))
            throw new ArgumentException("联系人不能为空", nameof(contactPerson));

        if (string.IsNullOrWhiteSpace(contactPhone))
            throw new ArgumentException("联系电话不能为空", nameof(contactPhone));

        return new Supplier
        {
            Code = code,
            Name = name,
            ContactPerson = contactPerson,
            ContactPhone = contactPhone,
            Email = email,
            Address = address,
            Status = SupplierStatus.Active,
            TaxNumber = taxNumber,
            BankAccount = bankAccount,
            BankName = bankName
        };
    }

    public void UpdateContactInfo(string contactPerson, string contactPhone, string email)
    {
        if (string.IsNullOrWhiteSpace(contactPerson))
            throw new ArgumentException("联系人不能为空", nameof(contactPerson));

        if (string.IsNullOrWhiteSpace(contactPhone))
            throw new ArgumentException("联系电话不能为空", nameof(contactPhone));

        ContactPerson = contactPerson;
        ContactPhone = contactPhone;
        Email = email;
    }

    public void UpdateBankInfo(string bankAccount, string bankName)
    {
        BankAccount = bankAccount;
        BankName = bankName;
    }

    public void Deactivate()
    {
        if (Status == SupplierStatus.Inactive)
            throw new InvalidOperationException("供应商已经处于停用状态");

        Status = SupplierStatus.Inactive;
    }

    public void Activate()
    {
        if (Status == SupplierStatus.Active)
            throw new InvalidOperationException("供应商已经处于启用状态");

        Status = SupplierStatus.Active;
    }
}

public enum SupplierStatus
{
    Active = 0,
    Inactive = 1
}