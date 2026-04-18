using FlatManage.Domain.Common;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public int UnitId { get; set; }
        public string NID { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string WorkAddress { get; set; } = string.Empty;
        public string? AgreementDocumentUrl { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
        public ICollection<RentInvoice> RentInvoices { get; set; } = new List<RentInvoice>();
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();
        public ICollection<SMSLog> SMSLogs { get; set; } = new List<SMSLog>();
    }
}
