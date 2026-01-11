
using System.ComponentModel.DataAnnotations;

namespace dotnetWebApiCoreCBA.Models.DTOs.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}