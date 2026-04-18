using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;

namespace FlatManage.Domain.Entities
{
    public class AmenityBooking : BaseEntity
    {
        public int AmenityId { get; set; }
        public int TenantId { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public BookingStatus Status { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }

        public Amenity Amenity { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
    }
}
