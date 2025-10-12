using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models;

public class PasswordChangeModel
{
    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}
