using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Core.Entities.Identity;

public class AppRole : IdentityRole
{
    public AppRole()
    {
        
    }
    public AppRole(string roleName) : base(roleName)
    {
    }
}
