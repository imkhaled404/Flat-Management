using FlatManage.Domain.Common;
using System;

namespace FlatManage.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string NID { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string? OwnershipDocumentUrl { get; set; }
        public string? Notes { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
