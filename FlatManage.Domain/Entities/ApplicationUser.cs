using Microsoft.AspNetCore.Identity;
using FlatManage.Domain.Enums;
using System;

namespace FlatManage.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? NID { get; set; }
        public string? ProfilePicture { get; set; }
        public UserType UserType { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
