using FlatManage.Domain.Common;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Floor : BaseEntity
    {
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalUnits { get; set; }

        public Building Building { get; set; } = null!;
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
