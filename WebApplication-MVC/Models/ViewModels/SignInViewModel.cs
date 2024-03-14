

using Infrastructure.Models;

namespace WebApplication_MVC.Models.Views;

public class SignInViewModel
{
    public SignInModel Form { get; set; } = new SignInModel();

    public string? ErrorMessage { get; set; }

}
