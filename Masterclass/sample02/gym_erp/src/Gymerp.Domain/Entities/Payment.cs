using System;

namespace Gymerp.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid EnrollmentId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DueDate { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public int Attempts { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public virtual Enrollment Enrollment { get; private set; }

        protected Payment() { } // Para o EF

        public Payment(Guid enrollmentId, decimal amount, DateTime dueDate)
        {
            Id = Guid.NewGuid();
            EnrollmentId = enrollmentId;
            Amount = amount;
            DueDate = dueDate;
            Status = PaymentStatus.Pending;
            Attempts = 0;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsPaid()
        {
            Status = PaymentStatus.Paid;
            ProcessedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsOverdue()
        {
            Status = PaymentStatus.Overdue;
            Attempts++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsCancelled()
        {
            Status = PaymentStatus.Cancelled;
            ProcessedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsApproved()
        {
            Status = PaymentStatus.Paid;
            ProcessedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsRejected()
        {
            Status = PaymentStatus.Rejected;
            ProcessedAt = DateTime.UtcNow;
            Attempts++;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Overdue,
        Cancelled,
        Rejected
    }
} 