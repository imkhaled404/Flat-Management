using FlatManage.Domain.Common;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Building : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalFloors { get; set; }
        public int TotalUnits { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Floor> Floors { get; set; } = new List<Floor>();
        public ICollection<Notice> Notices { get; set; } = new List<Notice>();
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
