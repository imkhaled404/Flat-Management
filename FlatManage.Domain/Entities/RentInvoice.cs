using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class RentInvoice : BaseEntity
    {
        public int TenantId { get; set; }
        public int UnitId { get; set; }
        public int AgreementId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int BillingMonth { get; set; }
        public int BillingYear { get; set; }
        public decimal RentAmount { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset? PaidDate { get; set; }
        public decimal PaidAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal LateFee { get; set; }
        public string? Notes { get; set; }

        public Tenant Tenant { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public Agreement Agreement { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
