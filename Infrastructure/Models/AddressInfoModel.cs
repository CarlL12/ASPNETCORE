using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AddressInfoModel
{

    public int Id { get; set; }


    [Display(Name = "Address line 1", Prompt = " Enter your first address line", Order = 0)]
    [DataType(DataType.Text)]   
    [Required(ErrorMessage = "Address is required")]
    public string Address1 { get; set; } = null!;


    [Display(Name = "Address line 2 (optional)", Prompt = " Enter your second address line ", Order = 1)]
    [DataType(DataType.Text)]
    public string? Address2 { get; set; }


    [Display(Name = "Postal code", Prompt = " Enter your postal code", Order = 2)]
    [DataType(DataType.PostalCode)]
    [Required(ErrorMessage = "Postal code is required")]
    public string PostalCode { get; set; } = null!;


    [Display(Name = "City", Prompt = " Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;



}
