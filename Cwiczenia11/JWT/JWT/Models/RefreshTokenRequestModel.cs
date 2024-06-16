using System.ComponentModel.DataAnnotations;

namespace JWT.Models;


public class RefreshTokenRequestModel
{
    [Required] public string RefreshToken { get; set; } = null!;
}
