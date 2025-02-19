using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales
{
    public class SalesChannel : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public ChannelType Type { get; private set; }
        public ChannelStatus Status { get; private set; }
        public decimal CommissionRate { get; private set; }
        public string ContactPerson { get; private set; }
        public string ContactPhone { get; private set; }
        public string ContactEmail { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }
        public List<SalesPerformance> Performances { get; private set; }

        private SalesChannel() { }

        public SalesChannel(
            string name,
            string code,
            string description,
            ChannelType type,
            decimal commissionRate,
            string contactPerson,
            string contactPhone,
            string contactEmail,
            string address)
        {
            Name = name;
            Code = code;
            Description = description;
            Type = type;
            Status = ChannelStatus.Active;
            CommissionRate = commissionRate;
            ContactPerson = contactPerson;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            Address = address;
            CreatedTime = DateTime.Now;
            Performances = new List<SalesPerformance>();
        }

        public void UpdateContactInfo(
            string contactPerson,
            string contactPhone,
            string contactEmail,
            string address)
        {
            ContactPerson = contactPerson;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            Address = address;
            LastModifiedTime = DateTime.Now;
        }

        public void UpdateCommissionRate(decimal newRate)
        {
            if (newRate < 0 || newRate > 100)
                throw new ArgumentException("Commission rate must be between 0 and 100", nameof(newRate));

            CommissionRate = newRate;
            LastModifiedTime = DateTime.Now;
        }

        public void Deactivate()
        {
            Status = ChannelStatus.Inactive;
            LastModifiedTime = DateTime.Now;
        }

        public void AddPerformance(SalesPerformance performance)
        {
            if (performance == null)
                throw new ArgumentNullException(nameof(performance));

            Performances.Add(performance);
        }
    }

    public enum ChannelType
    {
        Direct,
        Distributor,
        Retailer,
        Online,
        Agent
    }

    public enum ChannelStatus
    {
        Active,
        Inactive,
        Suspended
    }
}