using FlatManage.Domain.Common;

namespace FlatManage.Domain.Entities
{
    public class SocietyComment : BaseEntity
    {
        public int PostId { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public SocietyPost Post { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
    }
}
