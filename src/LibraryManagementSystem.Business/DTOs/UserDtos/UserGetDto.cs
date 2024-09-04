using LibraryManagementSystem.Core.Entities.Identity;

namespace LibraryManagementSystem.Business.DTOs.UserDtos;

public class UserGetDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}
