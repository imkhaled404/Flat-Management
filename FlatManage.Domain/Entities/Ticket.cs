using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int TenantId { get; set; }
        public int UnitId { get; set; }
        public int BuildingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketCategory Category { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public string? AssignedTo { get; set; }
        public DateTimeOffset? ResolvedAt { get; set; }
        public string? ImageUrl { get; set; }

        public Tenant Tenant { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public Building Building { get; set; } = null!;
        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
    }
}
