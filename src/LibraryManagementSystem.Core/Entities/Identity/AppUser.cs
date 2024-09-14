using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }

    public Author? Author { get; set; }
    public Publisher? Publisher { get; set; }
}
