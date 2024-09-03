using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Business.DTOs.UserDtos;

public class UserRegisterDto
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
