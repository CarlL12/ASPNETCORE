using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;

public class SecurityViewModel
{

    public SecurityInfoModel SecurityInfo { get; set; } = null!;

    public ProfileInfoModel Profile { get; set; } = null!;

    public BasicInfoModel BasicInfo { get; set; } = null!;

    public DeleteModel Delete { get; set; } = null!;
}
