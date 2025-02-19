using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales
{
    public class CustomerProfile : Entity, IAggregateRoot
    {
        public string CustomerId { get; private set; }
        public CustomerGrade Grade { get; private set; }
        public CreditRating CreditRating { get; private set; }
        public decimal CreditLimit { get; private set; }
        public decimal CurrentCredit { get; private set; }
        public List<SatisfactionSurvey> SatisfactionSurveys { get; private set; }
        public DateTime LastPurchaseDate { get; private set; }
        public decimal TotalPurchaseAmount { get; private set; }
        public int TotalOrders { get; private set; }
        public DateTime LastEvaluationDate { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }

        private CustomerProfile() { }

        public CustomerProfile(
            string customerId,
            CustomerGrade initialGrade,
            CreditRating initialCreditRating,
            decimal creditLimit)
        {
            CustomerId = customerId;
            Grade = initialGrade;
            CreditRating = initialCreditRating;
            CreditLimit = creditLimit;
            CurrentCredit = creditLimit;
            SatisfactionSurveys = new List<SatisfactionSurvey>();
            CreatedTime = DateTime.Now;
            LastEvaluationDate = DateTime.Now;
        }

        public void UpdateGrade(CustomerGrade newGrade)
        {
            Grade = newGrade;
            LastModifiedTime = DateTime.Now;
        }

        public void UpdateCreditRating(CreditRating newRating, decimal newCreditLimit)
        {
            if (newCreditLimit < 0)
                throw new ArgumentException("Credit limit cannot be negative.", nameof(newCreditLimit));

            CreditRating = newRating;
            CreditLimit = newCreditLimit;
            CurrentCredit = newCreditLimit;
            LastModifiedTime = DateTime.Now;
        }

        public void AddSatisfactionSurvey(SatisfactionSurvey survey)
        {
            if (survey == null)
                throw new ArgumentNullException(nameof(survey));

            SatisfactionSurveys.Add(survey);
            LastModifiedTime = DateTime.Now;
        }

        public void RecordPurchase(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Purchase amount cannot be negative.", nameof(amount));

            LastPurchaseDate = DateTime.Now;
            TotalPurchaseAmount += amount;
            TotalOrders++;
            LastModifiedTime = DateTime.Now;
        }

        public bool HasSufficientCredit(decimal amount)
        {
            return CurrentCredit >= amount;
        }

        public void DeductCredit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            if (amount > CurrentCredit)
                throw new InvalidOperationException("Insufficient credit available.");

            CurrentCredit -= amount;
            LastModifiedTime = DateTime.Now;
        }

        public void RestoreCredit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            CurrentCredit = Math.Min(CreditLimit, CurrentCredit + amount);
            LastModifiedTime = DateTime.Now;
        }
    }

    public class SatisfactionSurvey : Entity
    {
        public string OrderNumber { get; private set; }
        public int Rating { get; private set; }
        public string Comments { get; private set; }
        public DateTime SurveyDate { get; private set; }

        private SatisfactionSurvey() { }

        public SatisfactionSurvey(string orderNumber, int rating, string comments)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.", nameof(rating));

            OrderNumber = orderNumber;
            Rating = rating;
            Comments = comments;
            SurveyDate = DateTime.Now;
        }
    }

    public enum CustomerGrade
    {
        Regular,
        Silver,
        Gold,
        Platinum,
        Diamond
    }

    public enum CreditRating
    {
        Poor,
        Fair,
        Good,
        Excellent
    }
}