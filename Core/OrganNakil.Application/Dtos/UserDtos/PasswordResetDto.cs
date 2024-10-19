using System.ComponentModel.DataAnnotations;

namespace OrganNakil.Application.Dtos.UserDtos;

public class PasswordResetDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}