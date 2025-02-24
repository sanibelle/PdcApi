using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.Version;

public class SyllabusVersion : AVersion
{
    public Semester Semester { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? PublishedDate { get; set; }
    public bool IsPublished
    {
        get
        {
            return PublishedDate != null;
        }
    }
}
