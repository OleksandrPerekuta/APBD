using System.ComponentModel.DataAnnotations;

namespace JWT.Models;


public class RegisterRequestModel
{
    [Required] public string UserName { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}

