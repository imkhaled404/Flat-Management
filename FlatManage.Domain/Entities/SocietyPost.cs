using FlatManage.Domain.Common;
using FlatManage.Domain.Enums;
using System.Collections.Generic;

namespace FlatManage.Domain.Entities
{
    public class SocietyPost : BaseEntity
    {
        public int BuildingId { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public PostType PostType { get; set; }
        public bool IsApproved { get; set; }
        public int LikeCount { get; set; }

        public Building Building { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
        public ICollection<SocietyComment> Comments { get; set; } = new List<SocietyComment>();
    }
}
