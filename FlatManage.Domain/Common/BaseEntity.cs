using System;
using FlatManage.Domain.Interfaces;

namespace FlatManage.Domain.Common
{
    /// <summary>
    /// Base class for all domain entities.
    /// </summary>
    public abstract class BaseEntity : ISoftDelete
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
