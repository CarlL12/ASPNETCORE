using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;


public class DetailsViewModel
{
    public BasicInfoModel BasicInfo { get; set; } = null!;

    public ProfileInfoModel ProfileInfo { get; set; } = null!;
    public AddressInfoModel AddressInfo { get; set; } = null!;
}   
