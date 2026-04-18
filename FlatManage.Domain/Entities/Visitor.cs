using FlatManage.Domain.Common;
using System;

namespace FlatManage.Domain.Entities
{
    public class Visitor : BaseEntity
    {
        public int BuildingId { get; set; }
        public int? UnitId { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public string VisitorPhone { get; set; } = string.Empty;
        public string? VisitorNID { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string? VehicleNumber { get; set; }
        public DateTimeOffset CheckInTime { get; set; }
        public DateTimeOffset? CheckOutTime { get; set; }
        public int? HostTenantId { get; set; }
        public string? EntryPassCode { get; set; }
        public string? Photo { get; set; }
        public string AddedBy { get; set; } = string.Empty;

        public Building Building { get; set; } = null!;
        public Unit? Unit { get; set; }
        public Tenant? HostTenant { get; set; }
        public ApplicationUser Creator { get; set; } = null!;
    }
}
