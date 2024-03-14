

using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class DeleteModel
{
    [Display(Name = "Yes, I want to delete my account", Order = 5)]
    [CheckboxRequired(ErrorMessage = "Please check the checkbox before submitting.")]
    public bool Delete { get; set; } = false;
}
