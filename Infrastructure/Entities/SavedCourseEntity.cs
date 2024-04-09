

namespace Infrastructure.Entities;

public class SavedCourseEntity
{
    public string UserId { get; set; } = null!;

    public int CourseId { get; set; } 

    public UserEntity User { get; set; } = null!;

    public CourseEntity Course { get; set; } = null!;
}
