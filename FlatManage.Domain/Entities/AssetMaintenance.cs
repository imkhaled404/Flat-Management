using FlatManage.Domain.Common;
using System;

namespace FlatManage.Domain.Entities
{
    public class AssetMaintenance : BaseEntity
    {
        public int AssetId { get; set; }
        public DateTimeOffset MaintenanceDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
        public DateTimeOffset? NextMaintenanceDate { get; set; }

        public Asset Asset { get; set; } = null!;
    }
}
