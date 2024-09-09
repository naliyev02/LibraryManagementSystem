using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;

public class RegisterDto
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
