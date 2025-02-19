using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class Customer : AuditableEntity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string ContactPerson { get; private set; }
    public string ContactPhone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public CustomerStatus Status { get; private set; }
    public CustomerType Type { get; private set; }
    public decimal CreditLimit { get; private set; }
    public string TaxNumber { get; private set; }
    public string BankAccount { get; private set; }
    public string BankName { get; private set; }

    private Customer() { }

    public static Customer Create(
        string code,
        string name,
        string contactPerson,
        string contactPhone,
        CustomerType type,
        string email = null,
        string address = null,
        decimal creditLimit = 0,
        string taxNumber = null,
        string bankAccount = null,
        string bankName = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("客户编码不能为空", nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("客户名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(contactPerson))
            throw new ArgumentException("联系人不能为空", nameof(contactPerson));

        if (string.IsNullOrWhiteSpace(contactPhone))
            throw new ArgumentException("联系电话不能为空", nameof(contactPhone));

        if (creditLimit < 0)
            throw new ArgumentException("信用额度不能为负数", nameof(creditLimit));

        return new Customer
        {
            Code = code,
            Name = name,
            ContactPerson = contactPerson,
            ContactPhone = contactPhone,
            Email = email,
            Address = address,
            Status = CustomerStatus.Active,
            Type = type,
            CreditLimit = creditLimit,
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

    public void UpdateCreditLimit(decimal newCreditLimit)
    {
        if (newCreditLimit < 0)
            throw new ArgumentException("信用额度不能为负数", nameof(newCreditLimit));

        CreditLimit = newCreditLimit;
    }

    public void UpdateBankInfo(string bankAccount, string bankName)
    {
        BankAccount = bankAccount;
        BankName = bankName;
    }

    public void Deactivate()
    {
        if (Status == CustomerStatus.Inactive)
            throw new InvalidOperationException("客户已经处于停用状态");

        Status = CustomerStatus.Inactive;
    }

    public void Activate()
    {
        if (Status == CustomerStatus.Active)
            throw new InvalidOperationException("客户已经处于启用状态");

        Status = CustomerStatus.Active;
    }
}

public enum CustomerStatus
{
    Active = 0,
    Inactive = 1
}

public enum CustomerType
{
    Individual = 0,
    Company = 1,
    Government = 2
}