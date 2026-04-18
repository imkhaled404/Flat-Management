using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        public int BuildingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public AmenityType AmenityType { get; set; }
        public int Capacity { get; set; }
        public bool BookingRequired { get; set; }
        public decimal DailyRate { get; set; }

        public Building Building { get; set; } = null!;
        public ICollection<AmenityBooking> Bookings { get; set; } = new List<AmenityBooking>();
    }
}
