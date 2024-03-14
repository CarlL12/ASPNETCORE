using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [Required]
    [Display(Name = "First name")]
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public string? ProfileImage { get; set; }

    public bool IsExternalAccount { get; set; } = false;


    [ForeignKey(nameof(AddressEntity))]
    public int? AddressId { get; set; }

    public AddressEntity? Address { get; set; }
}
