using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;

public class SavedCoursesViewModel
{
    public ProfileInfoModel Profile { get; set; } = null!;

    public BasicInfoModel BasicInfo { get; set; } = null!;

    public CourseModel Course { get; set; } = null!;

    public DeleteModel Delete { get; set; } = null!;
}
