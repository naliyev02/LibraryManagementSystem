using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;

public class ForgotPasswordDto
{
    public string Email { get; set; }
}
