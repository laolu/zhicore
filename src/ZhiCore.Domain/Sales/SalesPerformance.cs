using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales
{
    public class SalesPerformance : Entity
    {
        public string SalesChannelId { get; private set; }
        public DateTime Period { get; private set; }
        public decimal TargetAmount { get; private set; }
        public decimal ActualAmount { get; private set; }
        public int TargetOrders { get; private set; }
        public int ActualOrders { get; private set; }
        public decimal CommissionEarned { get; private set; }
        public PerformanceStatus Status { get; private set; }
        public string Comments { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }

        private SalesPerformance() { }

        public SalesPerformance(
            string salesChannelId,
            DateTime period,
            decimal targetAmount,
            int targetOrders)
        {
            if (targetAmount < 0)
                throw new ArgumentException("Target amount cannot be negative.", nameof(targetAmount));
            if (targetOrders < 0)
                throw new ArgumentException("Target orders cannot be negative.", nameof(targetOrders));

            SalesChannelId = salesChannelId;
            Period = period;
            TargetAmount = targetAmount;
            TargetOrders = targetOrders;
            ActualAmount = 0;
            ActualOrders = 0;
            CommissionEarned = 0;
            Status = PerformanceStatus.InProgress;
            CreatedTime = DateTime.Now;
        }

        public void UpdateActualPerformance(
            decimal actualAmount,
            int actualOrders,
            decimal commissionRate)
        {
            if (actualAmount < 0)
                throw new ArgumentException("Actual amount cannot be negative.", nameof(actualAmount));
            if (actualOrders < 0)
                throw new ArgumentException("Actual orders cannot be negative.", nameof(actualOrders));

            ActualAmount = actualAmount;
            ActualOrders = actualOrders;
            CommissionEarned = actualAmount * (commissionRate / 100);
            LastModifiedTime = DateTime.Now;

            UpdateStatus();
        }

        public void AddComments(string comments)
        {
            if (string.IsNullOrWhiteSpace(comments))
                throw new ArgumentException("Comments cannot be empty.", nameof(comments));

            Comments = comments;
            LastModifiedTime = DateTime.Now;
        }

        private void UpdateStatus()
        {
            if (ActualAmount >= TargetAmount && ActualOrders >= TargetOrders)
                Status = PerformanceStatus.Exceeded;
            else if (ActualAmount >= TargetAmount * 0.8 && ActualOrders >= TargetOrders * 0.8)
                Status = PerformanceStatus.Achieved;
            else
                Status = PerformanceStatus.BelowTarget;
        }
    }

    public enum PerformanceStatus
    {
        InProgress,
        Exceeded,
        Achieved,
        BelowTarget
    }
}