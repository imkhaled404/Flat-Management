using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System;

namespace FlatManage.Domain.Entities
{
    public class Notice : BaseEntity
    {
        public int? BuildingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NoticeType NoticeType { get; set; }
        public string PublishedBy { get; set; } = string.Empty;
        public DateTimeOffset PublishedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ExpiresAt { get; set; }
        public string? AttachmentUrl { get; set; }

        public Building? Building { get; set; }
        public ApplicationUser Publisher { get; set; } = null!;
    }
}
