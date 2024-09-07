using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;

public class ResetPasswordDto
{
    public string UserId { get; set; }
    public string ResetToken { get; set; }

    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}
