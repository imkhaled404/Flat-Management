using FlatManage.Domain.Common;
using System;

namespace FlatManage.Domain.Entities
{
    public class TicketComment : BaseEntity
    {
        public int TicketId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public bool IsInternal { get; set; }

        public Ticket Ticket { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
