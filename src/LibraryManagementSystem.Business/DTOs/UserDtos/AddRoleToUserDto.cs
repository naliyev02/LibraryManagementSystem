﻿namespace LibraryManagementSystem.Business.DTOs.UserDtos;

public class AddRoleToUserDto
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}
