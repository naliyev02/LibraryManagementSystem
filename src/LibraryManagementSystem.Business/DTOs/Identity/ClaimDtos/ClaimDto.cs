namespace LibraryManagementSystem.Business.DTOs.Identity.ClaimDtos;

public class ClaimDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}
