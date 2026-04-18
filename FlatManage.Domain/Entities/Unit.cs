using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Unit : BaseEntity
    {
        public int FloorId { get; set; }
        public int BuildingId { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public UnitType UnitType { get; set; }
        public decimal SizeInSqFt { get; set; }
        public decimal MonthlyRent { get; set; }
        public UnitStatus Status { get; set; }
        public string? Description { get; set; }

        public Floor Floor { get; set; } = null!;
        public Building Building { get; set; } = null!;
        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
        public ICollection<RentInvoice> RentInvoices { get; set; } = new List<RentInvoice>();
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Visitor> Visitors { get; set; } = new List<Visitor>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
