namespace LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string UserName { get; set; }
}
