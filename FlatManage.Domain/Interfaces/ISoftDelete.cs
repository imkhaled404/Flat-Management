using System;

namespace FlatManage.Domain.Interfaces
{
    /// <summary>
    /// Interface for entities that support soft deletion.
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
