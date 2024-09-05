namespace LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;

public class LoginResponse
{
    public bool IsLogedIn { get; set; } = false;
    public string JwtToken { get; set; }
    public string RefreshToken { get; internal set; }
}
