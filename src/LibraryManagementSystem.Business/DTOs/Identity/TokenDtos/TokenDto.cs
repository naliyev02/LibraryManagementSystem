namespace LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime TokenExpire { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpire { get; set; }
}
