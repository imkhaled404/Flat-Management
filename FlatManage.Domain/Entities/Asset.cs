using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public int BuildingId { get; set; }
        public int? UnitId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string AssetType { get; set; } = string.Empty;
        public DateTimeOffset PurchaseDate { get; set; }
        public decimal PurchaseAmount { get; set; }
        public DateTimeOffset? WarrantyExpiry { get; set; }
        public string? SerialNumber { get; set; }
        public AssetCondition Condition { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public Building Building { get; set; } = null!;
        public Unit? Unit { get; set; }
        public ICollection<AssetMaintenance> MaintenanceRecords { get; set; } = new List<AssetMaintenance>();
    }
}
