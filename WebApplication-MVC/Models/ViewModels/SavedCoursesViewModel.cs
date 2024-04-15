using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;

public class SavedCoursesViewModel
{
    public ProfileInfoModel Profile { get; set; } = null!;

    public BasicInfoModel BasicInfo { get; set; } = null!;

    public List<CourseModel> Courses { get; set; } = null!;

}
