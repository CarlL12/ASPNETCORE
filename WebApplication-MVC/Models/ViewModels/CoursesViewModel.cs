
using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;

public class CoursesViewModel
{

    public IEnumerable<CategoryModel>? Categories { get; set; }

    public IEnumerable<CourseModel>? Courses { get; set; }
       
    public PaginationModel? Pageination { get; set; }
}
