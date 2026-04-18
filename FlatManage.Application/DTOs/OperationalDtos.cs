using System;

namespace FlatManage.Application.DTOs
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UnitNumber { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class VisitorDto
    {
        public int Id { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public string VisitorPhone { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTimeOffset CheckInTime { get; set; }
        public DateTimeOffset? CheckOutTime { get; set; }
        public string? UnitNumber { get; set; }
    }

    public class NoticeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string NoticeType { get; set; } = string.Empty;
        public DateTimeOffset PublishedAt { get; set; }
    }
}
