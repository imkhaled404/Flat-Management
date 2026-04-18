using System;
using System.Collections.Generic;

namespace FlatManage.Application.DTOs
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int TotalFloors { get; set; }
        public int TotalUnits { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class UnitDto
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty;
        public decimal SizeInSqFt { get; set; }
        public decimal MonthlyRent { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
