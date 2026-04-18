using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;

namespace FlatManage.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int? RentInvoiceId { get; set; }
        public int? BillId { get; set; }
        public int TenantId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? TransactionReference { get; set; }
        public string ReceivedBy { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public RentInvoice? RentInvoice { get; set; }
        public Bill? Bill { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
