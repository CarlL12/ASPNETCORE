

namespace Infrastructure.Models;

public class CourseResult
{
    public bool Succeeded { get; set; }

    public int TotalItems { get; set; }

    public int TotaltPages { get; set; }
    public IEnumerable<CourseModel>? Course { get; set; }
}
