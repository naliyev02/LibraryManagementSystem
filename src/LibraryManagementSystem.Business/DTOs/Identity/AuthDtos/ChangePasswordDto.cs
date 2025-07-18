﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;

public class ChangePasswordDto
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string CurrentPassword { get; set; }

    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}
