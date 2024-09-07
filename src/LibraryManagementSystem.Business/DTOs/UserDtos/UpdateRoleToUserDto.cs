namespace LibraryManagementSystem.Business.DTOs.UserDtos;

public class UpdateRoleToUserDto
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}
