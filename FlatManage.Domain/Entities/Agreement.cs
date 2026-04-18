using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Agreement : BaseEntity
    {
        public int TenantId { get; set; }
        public int UnitId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal AdvanceAmount { get; set; }
        public AgreementStatus Status { get; set; }
        public string? Terms { get; set; }
        public DateTimeOffset SignedDate { get; set; }

        public Tenant Tenant { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public ICollection<RentInvoice> RentInvoices { get; set; } = new List<RentInvoice>();
    }
}
