using System;

namespace Domain.Common
{
    public interface IAuditableEntity
    {
        Guid CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
