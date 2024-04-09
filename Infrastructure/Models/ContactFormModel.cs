

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactFormModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Name", Prompt = " Enter your full name", Order = 0)]

    public string Name { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email-address", Prompt = " Enter your email-address", Order = 1)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please select a service")]
    public string Service { get; set; } = null!;

    [DataType(DataType.MultilineText)]
    [Display(Name = "Message", Prompt = " Enter your message", Order = 2)]
    [Required(ErrorMessage = "You must write a message!")]
    public string Message { get; set; } = null!;

    public bool Succeeded { get; set; }

    public bool Sent {  get; set; }
}
