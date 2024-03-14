using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class BasicInfoModel
{


    public int? AddressId { get; set; } 


    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = " Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = " Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email-address", Prompt = " Enter your email-address", Order = 2)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone number (Optional)", Prompt = " Enter your phone number", Order = 3)]
    [DataType(DataType.PhoneNumber)]

    public string? PhoneNumber { get; set; }


    [Display(Name = "Bio (Optional)", Prompt = " Add a short bio...", Order = 4)]
    [DataType(DataType.MultilineText)]
    public string? Biography { get; set; }

    public bool IsExternalAccount { get; set; }



}
