using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models.DTOs;
public class Register
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}