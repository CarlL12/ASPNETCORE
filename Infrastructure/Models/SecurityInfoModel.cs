using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class SecurityInfoModel
{

    [DataType(DataType.Password)]
    [Display(Name = "Current password", Prompt = " Enter your new password", Order = 0)]
    [Required(ErrorMessage = "Invalid password")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "New password", Prompt = " Enter your new password", Order = 1)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password", Prompt = " Confirm your password", Order = 2)]
    [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;

}
